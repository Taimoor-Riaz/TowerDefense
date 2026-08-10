using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Entry behaviour on Bootstrap scene. Keeps services alive and loads Hub additively.
/// </summary>
public class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private SceneFlowConfig config;
    [SerializeField] private bool dontDestroyBootstrapRoot = true;

    private void Awake()
    {
        if (dontDestroyBootstrapRoot)
            DontDestroyOnLoad(gameObject);

        var services = GetComponent<GameServices>();
        if (services == null)
            services = gameObject.AddComponent<GameServices>();

        if (config != null)
        {
            var flow = GetComponent<SceneFlowService>();
            if (flow == null)
                flow = gameObject.AddComponent<SceneFlowService>();
            flow.SetConfig(config);
        }
    }

    private void Start()
    {
        SceneFlowConfig flowConfig = config;
        if (flowConfig == null && GameServices.Instance != null)
            flowConfig = GameServices.Instance.SceneFlowConfig;

        bool shouldLoadHub = flowConfig == null || flowConfig.loadHubOnBootstrapStart;
        if (!shouldLoadHub)
            return;

        string hubName = flowConfig != null ? flowConfig.hubSceneName : "Main_UI";
        Scene hub = SceneManager.GetSceneByName(hubName);
        if (hub.IsValid() && hub.isLoaded)
        {
            SceneManager.SetActiveScene(hub);
            return;
        }

        SceneFlowService flow = SceneFlowService.Instance;
        if (flow != null)
            flow.LoadHub();
        else
            SceneManager.LoadSceneAsync(hubName, LoadSceneMode.Additive);
    }
}

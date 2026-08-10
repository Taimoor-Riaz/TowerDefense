using UnityEngine;

/// <summary>
/// Persistent root for core services. Lives on Bootstrap (DontDestroyOnLoad).
/// </summary>
public class GameServices : MonoBehaviour
{
    public static GameServices Instance { get; private set; }

    [SerializeField] private SceneFlowConfig sceneFlowConfig;
    [SerializeField] private SceneFlowService sceneFlow;
    [SerializeField] private MobileQualityService mobileQuality;

    private static SceneFlowConfig _pendingConfig;

    public SceneFlowConfig SceneFlowConfig => sceneFlowConfig;
    public SceneFlowService SceneFlow => sceneFlow != null ? sceneFlow : SceneFlowService.Instance;
    public MobileQualityService MobileQuality => mobileQuality != null ? mobileQuality : MobileQualityService.Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_pendingConfig != null && sceneFlowConfig == null)
            sceneFlowConfig = _pendingConfig;
        _pendingConfig = null;

        if (sceneFlow == null)
            sceneFlow = GetComponent<SceneFlowService>();

        if (sceneFlow == null)
            sceneFlow = gameObject.AddComponent<SceneFlowService>();

        if (sceneFlowConfig != null)
            sceneFlow.SetConfig(sceneFlowConfig);

        if (mobileQuality == null)
            mobileQuality = GetComponent<MobileQualityService>();

        if (mobileQuality == null)
            mobileQuality = gameObject.AddComponent<MobileQualityService>();
    }

    public void ApplyConfig(SceneFlowConfig flowConfig)
    {
        if (flowConfig == null)
            return;

        sceneFlowConfig = flowConfig;
        if (sceneFlow == null)
            sceneFlow = GetComponent<SceneFlowService>();
        if (sceneFlow != null)
            sceneFlow.SetConfig(flowConfig);
    }

    /// <summary>
    /// Ensures services exist when entering play from Hub/Battle without Bootstrap (editor convenience).
    /// Product builds should start from Bootstrap.
    /// </summary>
    public static GameServices EnsureExists(SceneFlowConfig fallbackConfig = null)
    {
        if (Instance != null)
        {
            if (fallbackConfig != null)
                Instance.ApplyConfig(fallbackConfig);
            return Instance;
        }

        if (fallbackConfig == null)
            fallbackConfig = Resources.Load<SceneFlowConfig>("SceneFlowConfig");

        _pendingConfig = fallbackConfig;
        var go = new GameObject("GameServices (Runtime)");
        return go.AddComponent<GameServices>();
    }
}

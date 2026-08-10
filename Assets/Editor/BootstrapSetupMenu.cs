#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Day 1 helper: verify Bootstrap is index 0 and SceneFlowConfig is assigned.
/// Menu: Game / Foundation / Validate Bootstrap Setup
/// </summary>
public static class BootstrapSetupMenu
{
    private const string BootstrapPath = "Assets/Scenes/Bootstrap.unity";
    private const string ConfigPath = "Assets/Content/Config/SceneFlowConfig.asset";

    [MenuItem("Game/Foundation/Validate Bootstrap Setup")]
    public static void ValidateBootstrapSetup()
    {
        var config = AssetDatabase.LoadAssetAtPath<SceneFlowConfig>(ConfigPath);
        if (config == null)
        {
            EditorUtility.DisplayDialog("Bootstrap", "Missing SceneFlowConfig at:\n" + ConfigPath, "OK");
            return;
        }

        bool bootstrapInBuild = false;
        int bootstrapIndex = -1;
        var scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path == BootstrapPath && scenes[i].enabled)
            {
                bootstrapInBuild = true;
                bootstrapIndex = i;
                break;
            }
        }

        string message =
            "SceneFlowConfig: OK\n" +
            "Hub: " + config.hubSceneName + "\n" +
            "Battle: " + config.battleSceneName + "\n" +
            "Bootstrap in Build Settings: " + (bootstrapInBuild ? ("YES (index " + bootstrapIndex + ")") : "NO") + "\n\n" +
            (bootstrapIndex == 0
                ? "Build order looks correct (Bootstrap first)."
                : "Warning: Bootstrap should be Build Settings index 0.");

        Debug.Log("[Foundation] " + message.Replace("\n", " | "));
        EditorUtility.DisplayDialog("Bootstrap Validation", message, "OK");
    }

    [MenuItem("Game/Foundation/Open Bootstrap Scene")]
    public static void OpenBootstrapScene()
    {
        if (!System.IO.File.Exists(BootstrapPath))
        {
            EditorUtility.DisplayDialog("Bootstrap", "Scene missing:\n" + BootstrapPath, "OK");
            return;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(BootstrapPath, OpenSceneMode.Single);
    }
}
#endif

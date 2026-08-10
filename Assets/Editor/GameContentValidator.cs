#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Validates config + content catalogs before Play / M2 authoring.
/// Menu: Game → Foundation → Validate Game Content
/// </summary>
public static class GameContentValidator
{
    [MenuItem("Game/Foundation/Validate Game Content")]
    public static void ValidateGameContent()
    {
        var sb = new StringBuilder();
        int errors = 0;
        int warnings = 0;

        ValidateBuildSettings(sb, ref errors, ref warnings);
        ValidateRegistry(sb, ref errors, ref warnings);
        ValidateActiveAbilities(sb, ref errors, ref warnings);
        ValidateEnemies(sb, ref errors, ref warnings);
        ValidateWaveTables(sb, ref errors, ref warnings);

        string summary = "Errors: " + errors + " | Warnings: " + warnings + "\n\n" + sb;
        Debug.Log("[ContentValidator]\n" + summary);
        EditorUtility.DisplayDialog(
            errors == 0 ? "Content Validation OK" : "Content Validation Failed",
            summary.Length > 1500 ? summary.Substring(0, 1500) + "\n…" : summary,
            "OK");
    }

    private static void ValidateBuildSettings(StringBuilder sb, ref int errors, ref int warnings)
    {
        sb.AppendLine("== Build Settings ==");
        var scenes = EditorBuildSettings.scenes;
        if (scenes == null || scenes.Length == 0)
        {
            errors++;
            sb.AppendLine("ERROR: No scenes in Build Settings.");
            return;
        }

        if (!scenes[0].enabled || scenes[0].path != "Assets/Scenes/Bootstrap.unity")
        {
            errors++;
            sb.AppendLine("ERROR: Bootstrap.unity must be Build Settings index 0.");
        }
        else
            sb.AppendLine("OK: Bootstrap index 0");

        bool hasHub = false;
        bool hasBattle = false;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (!scenes[i].enabled)
                continue;
            if (scenes[i].path.EndsWith("Main_UI.unity"))
                hasHub = true;
            if (scenes[i].path.EndsWith("BattleScene.unity"))
                hasBattle = true;
        }

        if (!hasHub)
        {
            errors++;
            sb.AppendLine("ERROR: Main_UI.unity missing from Build Settings.");
        }
        else
            sb.AppendLine("OK: Main_UI present");

        if (!hasBattle)
        {
            errors++;
            sb.AppendLine("ERROR: BattleScene.unity missing from Build Settings.");
        }
        else
            sb.AppendLine("OK: BattleScene present");
    }

    private static void ValidateRegistry(StringBuilder sb, ref int errors, ref int warnings)
    {
        sb.AppendLine("== Config ==");
        var registry = AssetDatabase.LoadAssetAtPath<GameConfigRegistry>("Assets/Content/Config/GameConfigRegistry.asset");
        var resourcesRegistry = Resources.Load<GameConfigRegistry>("GameConfigRegistry");
        if (registry == null)
        {
            errors++;
            sb.AppendLine("ERROR: Missing Content GameConfigRegistry.");
            return;
        }

        sb.AppendLine("OK: Content GameConfigRegistry");
        if (resourcesRegistry == null)
        {
            errors++;
            sb.AppendLine("ERROR: Missing Resources/GameConfigRegistry.asset");
        }
        else
            sb.AppendLine("OK: Resources GameConfigRegistry");

        if (registry.SceneFlow == null)
        {
            errors++;
            sb.AppendLine("ERROR: Registry.SceneFlow missing");
        }
        if (registry.GameBalance == null)
        {
            errors++;
            sb.AppendLine("ERROR: Registry.GameBalance missing");
        }
        if (registry.MobileQuality == null)
        {
            errors++;
            sb.AppendLine("ERROR: Registry.MobileQuality missing");
        }
        else
        {
            if (registry.MobileQuality.low == null || registry.MobileQuality.mid == null || registry.MobileQuality.high == null)
            {
                errors++;
                sb.AppendLine("ERROR: MobileQuality catalog missing Low/Mid/High profile");
            }
            else
                sb.AppendLine("OK: Quality Low/Mid/High");
        }

        if (registry.ActiveAbilities == null)
            warnings++;
        if (registry.DefaultWaveTable == null)
            warnings++;
    }

    private static void ValidateActiveAbilities(StringBuilder sb, ref int errors, ref int warnings)
    {
        sb.AppendLine("== Active Abilities ==");
        string[] guids = AssetDatabase.FindAssets("t:ActiveAbilityDefinition");
        var ids = new HashSet<string>();
        var catalog = AssetDatabase.LoadAssetAtPath<ActiveAbilityCatalog>("Assets/Content/Abilities/ActiveAbilityCatalog.asset");
        var inCatalog = new HashSet<ActiveAbilityDefinition>();
        if (catalog != null && catalog.abilities != null)
        {
            for (int i = 0; i < catalog.abilities.Length; i++)
            {
                if (catalog.abilities[i] != null)
                    inCatalog.Add(catalog.abilities[i]);
            }
        }

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var def = AssetDatabase.LoadAssetAtPath<ActiveAbilityDefinition>(path);
            if (def == null)
                continue;

            if (string.IsNullOrWhiteSpace(def.id) || def.id == "active_unnamed")
            {
                errors++;
                sb.AppendLine("ERROR: Missing id — " + path);
            }
            else if (!ids.Add(def.id))
            {
                errors++;
                sb.AppendLine("ERROR: Duplicate ability id '" + def.id + "' — " + path);
            }

            if (string.IsNullOrWhiteSpace(def.displayName) || def.displayName == "Unnamed Active")
            {
                warnings++;
                sb.AppendLine("WARN: Missing display name — " + path);
            }

            if (def.icon == null)
            {
                warnings++;
                sb.AppendLine("WARN: Missing icon — " + path);
            }

            if (def.cooldownSeconds < 0.1f)
            {
                errors++;
                sb.AppendLine("ERROR: Invalid cooldown — " + path);
            }

            if (catalog != null && !inCatalog.Contains(def) && def.includedInLaunchPool)
            {
                warnings++;
                sb.AppendLine("WARN: Launch ability not in catalog — " + path);
            }
        }

        sb.AppendLine("Checked " + guids.Length + " ActiveAbilityDefinition assets.");
    }

    private static void ValidateEnemies(StringBuilder sb, ref int errors, ref int warnings)
    {
        sb.AppendLine("== Enemy Definitions ==");
        string[] guids = AssetDatabase.FindAssets("t:EnemyDefinition");
        var ids = new HashSet<string>();
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var def = AssetDatabase.LoadAssetAtPath<EnemyDefinition>(path);
            if (def == null)
                continue;

            if (string.IsNullOrWhiteSpace(def.id) || def.id == "enemy_unnamed")
            {
                errors++;
                sb.AppendLine("ERROR: Missing enemy id — " + path);
            }
            else if (!ids.Add(def.id))
            {
                errors++;
                sb.AppendLine("ERROR: Duplicate enemy id '" + def.id + "'");
            }

            if (def.prefab == null)
            {
                warnings++;
                sb.AppendLine("WARN: Missing prefab — " + path);
            }

            if (def.maxHealth < 1f)
            {
                errors++;
                sb.AppendLine("ERROR: Invalid HP — " + path);
            }

            if (def.moveSpeed <= 0f)
            {
                errors++;
                sb.AppendLine("ERROR: Invalid speed — " + path);
            }

            if (def.behaviorId == EnemyBehaviorId.None)
            {
                warnings++;
                sb.AppendLine("WARN: Behavior None — " + path);
            }
        }

        sb.AppendLine("Checked " + guids.Length + " EnemyDefinition assets.");
    }

    private static void ValidateWaveTables(StringBuilder sb, ref int errors, ref int warnings)
    {
        sb.AppendLine("== Wave Tables ==");
        string[] guids = AssetDatabase.FindAssets("t:WaveTable");
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var table = AssetDatabase.LoadAssetAtPath<WaveTable>(path);
            if (table == null || table.waves == null)
                continue;

            var waveIds = new HashSet<string>();
            for (int w = 0; w < table.waves.Length; w++)
            {
                WaveDefinition wave = table.waves[w];
                if (wave == null)
                    continue;

                if (string.IsNullOrWhiteSpace(wave.waveId) || !waveIds.Add(wave.waveId))
                {
                    errors++;
                    sb.AppendLine("ERROR: Duplicate/missing waveId in " + path + " index " + w);
                }

                if (wave.isBossWave)
                {
                    bool hasEnemy = wave.enemies != null && wave.enemies.Length > 0;
                    if (!hasEnemy)
                    {
                        errors++;
                        sb.AppendLine("ERROR: Boss wave with no enemies — " + wave.waveId);
                    }
                }

                if (wave.enemies == null)
                    continue;

                for (int e = 0; e < wave.enemies.Length; e++)
                {
                    WaveEnemyEntry entry = wave.enemies[e];
                    if (entry == null)
                        continue;
                    if (entry.enemy == null)
                    {
                        errors++;
                        sb.AppendLine("ERROR: Wave entry missing EnemyDefinition — " + wave.waveId);
                    }
                    if (entry.count <= 0)
                    {
                        errors++;
                        sb.AppendLine("ERROR: Wave entry count <= 0 — " + wave.waveId);
                    }
                }
            }
        }

        sb.AppendLine("Checked " + guids.Length + " WaveTable assets.");
    }
}
#endif

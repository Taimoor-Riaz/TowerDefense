# Competitive Survival Merge Defense

Unity **6000.3.10f1** · URP 2D · Android portrait tower-defense / merge survival MVP.

## Open the project

1. Install Unity Hub + Editor **6000.3.10f1** (see `ProjectSettings/ProjectVersion.txt`).
2. Open this folder as a Unity project.
3. Wait for script compile / URP import.

## Play mode (correct entry)

**Always start from `Assets/Scenes/Bootstrap.unity`** (Build Settings index 0).

Flow:

1. `Bootstrap` — persistent `GameServices` + `SceneFlowService`
2. Loads **`Main_UI`** additively (hub)
3. Battle loads **`BattleScene`** additively via SceneFlow (hub unloads)

Build Settings order:

0. `Assets/Scenes/Bootstrap.unity`
1. `Assets/Scenes/Main_UI.unity`
2. `Assets/Scenes/BattleScene.unity`

Editor convenience: you can still open `Main_UI` / `BattleScene` alone for art work; product path is Bootstrap-first. Player builds require SceneFlow.

## Architecture (config-first)

See:

- [`Assets/Documentation/ScalableArchitecture.md`](Assets/Documentation/ScalableArchitecture.md)
- [`Assets/Documentation/MilestoneCrosswalk.md`](Assets/Documentation/MilestoneCrosswalk.md)
- [`Assets/Documentation/PoolingRules.md`](Assets/Documentation/PoolingRules.md)
- [`Assets/Documentation/AddressablesFoundation.md`](Assets/Documentation/AddressablesFoundation.md)

**Single registry:** `Assets/Content/Resources/GameConfigRegistry.asset`  
Flow: `GameConfigRegistry` → `GameBalanceConfig` → `ManaManager`

Content lives under `Assets/Content/` (balance, quality, abilities, enemies, waves, config).  
Scene names: `Assets/Content/Config/SceneFlowConfig.asset`.

Validate: **Game → Foundation → Validate Game Content**

## Core scripts

| Script | Role |
|--------|------|
| `Assets/Script/Core/GameServices.cs` | DontDestroy root |
| `Assets/Script/Core/GameConfigRegistry.cs` | Config entry |
| `Assets/Script/Core/SceneFlowService.cs` | Additive Hub/Battle load-unload |
| `Assets/Script/Core/BootstrapLoader.cs` | Bootstrap entry → load Hub |
| `Assets/Script/Core/GameplayEvents.cs` | Typed event bus |
| `Assets/Script/Core/PoolService.cs` | Generic VFX pool (M2+) |

## Android build

1. **Game → Foundation → Validate Android Player Settings**
2. File → Build Settings → Android (Development Build for smoke)
3. Scenes: Bootstrap → Main_UI → BattleScene
4. Build APK; follow [`Assets/Documentation/Android_BuildSmoke.md`](Assets/Documentation/Android_BuildSmoke.md) on a **physical device**

Package: `com.competitivesurvival.mergedefense` · Min SDK 24 · Target SDK 34

## Mobile quality

Low / Mid / High profiles under `Assets/Content/Quality/`.  
See [`MobileOptimizationChecklist.md`](Assets/Documentation/MobileOptimizationChecklist.md).

## Milestone 1 status

M1 foundation engineering (10 working days schedule) is complete pending:

- Editor smoke from Bootstrap
- Physical Android smoke
- Addressables: run **Initialize Addressables Groups** once and commit `Assets/AddressableAssetsData/`

**Next:** [`M2_Backlog_AbilityCorrection.md`](Assets/Documentation/M2_Backlog_AbilityCorrection.md)

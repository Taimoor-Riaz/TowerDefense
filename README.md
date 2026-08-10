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
3. Battle loads **`BattleScene`** additively (Day 3 wires UI buttons fully)

Build Settings order:

0. `Assets/Scenes/Bootstrap.unity`
1. `Assets/Scenes/Main_UI.unity`
2. `Assets/Scenes/BattleScene.unity`

Editor convenience: you can still open `Main_UI` / `BattleScene` alone for art work; product path is Bootstrap-first.

## Architecture (config-first)

See:

- [`Assets/Documentation/ScalableArchitecture.md`](Assets/Documentation/ScalableArchitecture.md) — rules: content in ScriptableObjects, additive scenes, mobile tiers
- [`Assets/Documentation/MilestoneCrosswalk.md`](Assets/Documentation/MilestoneCrosswalk.md) — client M1–M5 ↔ audit phases
- [`Assets/Documentation/ProjectArchitectureAndStatus.md`](Assets/Documentation/ProjectArchitectureAndStatus.md) — full audit

Content lives under `Assets/Content/` (balance, quality, abilities, enemies, waves, config).

Scene names / bootstrap behaviour: `Assets/Content/Config/SceneFlowConfig.asset`.

## Core scripts (Day 1)

| Script | Role |
|--------|------|
| `Assets/Script/Core/GameServices.cs` | DontDestroy root |
| `Assets/Script/Core/SceneFlowService.cs` | Additive Hub/Battle load-unload |
| `Assets/Script/Core/SceneFlowConfig.cs` | SO config |
| `Assets/Script/Core/BootstrapLoader.cs` | Bootstrap entry → load Hub |

## Android build (Day 4 baseline)

1. **Game → Foundation → Validate Android Player Settings**
2. File → Build Settings → Android (Development Build for smoke)
3. Scenes: Bootstrap → Main_UI → BattleScene
4. Build APK; follow [`Assets/Documentation/Android_BuildSmoke.md`](Assets/Documentation/Android_BuildSmoke.md)

Package: `com.competitivesurvival.mergedefense` · Min SDK 24 · Target SDK 34

## Mobile quality

Low / Mid / High profiles under `Assets/Content/Quality/`.  
Service applies FPS + VFX budget flags at boot. See [`MobileOptimizationChecklist.md`](Assets/Documentation/MobileOptimizationChecklist.md).

## Milestone 1 status

- **Day 1 (done):** architecture docs, Content folders, Bootstrap + SceneFlow scaffolding, Build Settings, README  
- **Day 2 (done):** NamingMap, Option A, AbilityInventory, SO templates  
- **Day 3 (done):** Additive Hub↔Battle, ability quarantine, GameBalanceConfig → ManaManager  
- **Day 4 (done):** Mobile quality tiers + Android Player Settings baseline + smoke docs  
- **Day 5 (done):** Acceptance pack — see [`Assets/Documentation/M1_DeliveryIndex.md`](Assets/Documentation/M1_DeliveryIndex.md)

**Milestone 1 engineering delivery is complete.** Remaining for client close-out: NamingMap sign-off + physical Android smoke APK.  
**Next:** [`M2_Backlog_AbilityCorrection.md`](Assets/Documentation/M2_Backlog_AbilityCorrection.md)  

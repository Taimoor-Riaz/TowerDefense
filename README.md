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

## Android build (baseline)

1. File → Build Settings → Android  
2. Ensure scenes listed above are enabled  
3. Player Settings: company/product/bundle id (cleaned in Day 4)  
4. Build debug APK and smoke: Bootstrap → Hub → (Battle when wired)

## Product lock (Option A)

- Pre-match: **6 units + 2 global actives**
- Unit skills: **passives / identity**
- Tower-selected ability buttons: **non-product** (quarantine Day 3)

## Milestone 1 status

- **Day 1 (done):** architecture docs, Content folders, Bootstrap + SceneFlow scaffolding, Build Settings, README  
- **Day 2 (done):** [NamingMap](Assets/Documentation/NamingMap.md), [Option A](Assets/Documentation/OptionA_AbilityArchitecture.md), [AbilityInventory](Assets/Documentation/AbilityInventory.md), SO templates + Content stubs (abilities/enemies/waves/balance)  
- **Day 3:** Wire UI to SceneFlow, quarantine abilities, mana clarity + `GameBalanceConfig`  
- **Day 4:** Mobile quality tiers + Android smoke  
- **Day 5:** Acceptance pack  

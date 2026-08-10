# Scalable Architecture Constitution

**Milestone:** Client M1 — Project Foundation & Technical Setup  
**Status:** Day 1 — locked for all future work  
**Engine:** Unity 6000.3.x · URP 2D · Android portrait

## Purpose

Make the game **scalable**, **extendable**, and **fixable without code** for content and balance. Code owns systems; data owns numbers and catalogs.

## Golden rules

1. **Config-first** — gameplay numbers, content lists, scene names, and quality settings live in ScriptableObjects under `Assets/Content/`.
2. **No new magic numbers in gameplay code** — if a value can be tuned, it belongs in an SO. Hardcoded constants are only for true engine invariants (e.g. merge max level 6 if GDD-locked — still prefer `GameBalanceConfig`).
3. **Systems read data** — managers/services consume SO definitions; they do not own content arrays that designers must find in scenes.
4. **Additive scenes only (product path)** — Bootstrap stays loaded; Hub and Battle load/unload additively via `SceneFlowService`. Do not use single-scene `SceneManager.LoadScene` for Hub↔Battle after Day 3 migration.
5. **Option A ability model** — 2 global actives from pre-match loadout; unit skills are passives/identity. Tower-selected ability buttons are non-product.
6. **Mobile tiers** — Low / Mid / High profiles drive FPS and VFX caps. All devices must run via a profile.

## Folder map

```
Assets/
  Content/                 ← designer-editable data (preferred)
    Units/                 ← UnitData assets (may also live where they already are during migration)
    Abilities/             ← ActiveAbilityDefinition catalog (M2 fills)
    Enemies/               ← EnemyDefinition catalog
    Waves/                 ← WaveTable assets
    Balance/               ← GameBalanceConfig
    Quality/               ← MobileQuality Low/Mid/High
    Config/                ← SceneFlowConfig and similar
  Script/
    Core/                  ← Bootstrap, SceneFlow, GameServices, quality
    … existing modules …
  Scenes/
    Bootstrap.unity        ← persistent root (Build Settings index 0)
    Main_UI.unity          ← hub (additive)
    BattleScene.unity      ← match (additive)
```

## Scene flow

```
Bootstrap (DontDestroy services)
  → LoadAdditive Main_UI
  → LoadAdditive BattleScene (unload when returning to hub)
```

API: `SceneFlowService.LoadHubAsync()`, `LoadBattleAsync()`, `UnloadBattleAsync()`, `ReloadBattleAsync()`.

## Content change workflow (no code)

| Change | Edit this |
|--------|-----------|
| Unit stats / prefabs | `UnitData` SO |
| Mana / summon cost curve | `GameBalanceConfig` |
| Active ability list / cooldowns | `ActiveAbilityDefinition` (M2+) |
| Enemy HP / behavior id | `EnemyDefinition` (M2+) |
| Wave composition | `WaveTable` (M2+) |
| Target FPS / VFX caps | `MobileQualityProfile` |
| Scene names | `SceneFlowConfig` |

If a balance tweak requires opening a `.cs` file, stop and move the value into Content.

## What code is for

- Rules engines (merge validity, combat resolution, status effects)
- Presentation (UI binding, VFX playback from pooled prefabs)
- Persistence adapters (save/load)
- Platform (Android, input, safe area)

## Explicitly deferred (enabled by this foundation)

- Filling 6 global actives (client M2)
- Enemy behavior components for full roster
- Progression / leagues / cards
- Addressables remote CDN (local Content first; Addressables can wrap same SOs later)

## Acceptance (architecture)

- [x] This document exists and is the team rule set
- [x] Content folders exist (Day 1); SO type templates Day 2
- [x] Bootstrap + SceneFlowService scaffolding (Day 1); full UI migration Day 3
- [ ] At least one live tunable driven by SO (Day 3–4)
- [ ] Mobile quality profiles apply (Day 4)

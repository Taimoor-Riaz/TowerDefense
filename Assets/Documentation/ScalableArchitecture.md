# Scalable Architecture Constitution

**Milestone:** Client M1 — Project Foundation & Technical Setup  
**Status:** Post-M1 hardening — config, save, events, quality consumers, pooling, Addressables foundation  
**Engine:** Unity 6000.3.x · URP 2D · Android portrait

## Purpose

Make the game **scalable**, **extendable**, and **tunable without code** for content and balance. Code owns systems; data owns numbers and catalogs.

## Golden rules

1. **Config-first** — gameplay numbers, content lists, scene names, and quality settings live in ScriptableObjects under `Assets/Content/`.
2. **One registry** — `GameConfigRegistry` is the only runtime entry point (`Resources/GameConfigRegistry.asset` references Content assets; do not duplicate balance/quality/scene configs under Resources).
3. **No new magic numbers in gameplay code** — if a value can be tuned, it belongs in an SO.
4. **Systems read data** — managers/services consume SO definitions via `GameServices.Instance.Config`.
5. **Additive scenes only (product path)** — Bootstrap stays loaded; Hub and Battle load/unload additively via `SceneFlowService`. Public API: `LoadHub()`, `LoadBattle()`, `ReloadBattle()`.
6. **Option A ability model** — 2 global actives from pre-match loadout; unit skills are passives/identity.
7. **Mobile tiers** — Low / Mid / High drive FPS + VFX via `MobileQualityRuntime` / `OnQualityChanged` (see [MobileOptimizationChecklist.md](MobileOptimizationChecklist.md)).
8. **No new direct PlayerPrefs** — use `GameServices.Instance.Save` (`ISaveService`). Legacy Currency/GameOver prefs migrate in M4.
9. **GameplayEvents for new content** — subscribe to typed `GameplayEvents` instead of coupling new abilities to managers.
10. **Pool combat FX** — no repeated Instantiate/Destroy in combat; use `PoolService` / specialized pools ([PoolingRules.md](PoolingRules.md)).
11. **Addressables Load → Use → Release** for heavy content ([AddressablesFoundation.md](AddressablesFoundation.md)).
12. **Validate before Play** — `Game → Foundation → Validate Game Content`.

## Config access

```
GameServices.Instance.Config
  ├── SceneFlow
  ├── GameBalance
  ├── MobileQuality
  ├── ActiveAbilities
  └── DefaultWaveTable
```

Edit balance only at `Assets/Content/Balance/GameBalanceConfig.asset`.

## Scene flow

```
Bootstrap (DontDestroy services)
  → LoadHub (Main_UI additive)
  → LoadBattle (unload Hub, load Battle)
  → LoadHub / ReloadBattle
```

API: `LoadHub()`, `LoadBattle()`, `ReloadBattle()`.

## Save

- Interface: `ISaveService` / `PlayerPrefsSaveService`
- Keys: `SaveKeys`
- Full `PlayerSaveData` JSON / cloud = Milestone 4

## Gameplay events

Typed publish-only bus: `GameplayEvents` (BattleStarted/Ended, WaveStarted, Enemy*, UnitSummoned/Merged, DamageDealt, BossSpawned, StatusApplied).
New M2 passives/actives subscribe here.

## Folder map

```
Assets/
  Content/                 ← designer-editable data (canonical)
    Config/                ← GameConfigRegistry + SceneFlowConfig
    Balance/, Quality/, Abilities/, Enemies/, Waves/, Units/
  Resources/
    GameConfigRegistry.asset   ← ONLY config entry in Resources (refs Content)
  Script/Core/             ← GameServices, SceneFlow, Save, GameplayEvents, Pool, Addressables
  Scenes/Bootstrap.unity   ← Build Settings index 0
```

## Content change workflow (no code)

| Change | Edit this (one place) |
|--------|------------------------|
| Mana / summon cost | `Content/Balance/GameBalanceConfig` |
| Active abilities | `Content/Abilities/*` via catalog |
| Quality FPS/VFX | `Content/Quality/MobileQuality_*` |
| Scene names | `Content/Config/SceneFlowConfig` |
| Registry wiring | `Content/Config/GameConfigRegistry` |

## Explicitly deferred

- Filling 6 global actives (M2)
- Enemy behavior components / full progression save JSON (M3–M4)
- Migrating CurrencyManager Gold/Gems off PlayerPrefs (M4)
- Remote Addressables CDN

## Acceptance (architecture)

- [x] GameConfigRegistry single source of truth
- [x] Summon cost owned by ManaManager only
- [x] ISaveService for new persistence
- [x] SceneFlow public API simplified
- [x] GameplayEvents bridged from core raise sites
- [x] MobileQuality wired to shake / VFX / particles / damage numbers / status + `OnQualityChanged`
- [x] PoolService + pooling rules for M2
- [x] Local Addressables groups + `AddressableContent` helper
- [x] Game Content validator menu

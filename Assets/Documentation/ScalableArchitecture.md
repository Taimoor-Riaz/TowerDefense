# Scalable Architecture Constitution

**Milestone:** Client M1 — Project Foundation & Technical Setup  
**Status:** M1 remaining close-out — GUI pack imported; Arena/HUD swap Days 2–4  
**Engine:** Unity 6000.3.x · URP 2D · **Android portrait** (iOS not in MVP)  
**Schedule:** 10 working days M1 (architecture done; 5 days remaining)

## Purpose

Make the game **scalable**, **extendable**, and **tunable without code** for content and balance. Code owns systems; data owns numbers and catalogs.

## Golden rules

1. **Config-first** — gameplay numbers, content lists, scene names, and quality settings live in ScriptableObjects under `Assets/Content/`.
2. **One registry** — only `Assets/Content/Resources/GameConfigRegistry.asset` (Resources.Load + Content layout). Do not duplicate balance/quality/scene configs under `Assets/Resources/`.
3. **No new magic numbers in gameplay code** — if a value can be tuned, it belongs in an SO.
4. **Systems read data** — managers/services consume SO definitions via `GameServices.Instance.Config`.
5. **Additive scenes only (product path)** — Bootstrap stays loaded; Hub and Battle load/unload additively via `SceneFlowService`. Public API: `LoadHub()`, `LoadBattle()`, `ReloadBattle()`.
6. **Option A** — in this order: 6 units pre-match, 2 global actives pre-match, HUD buttons independent of selected tower, unit skills passive/identity, tower-selected ability UX is not the product path.
7. **Mobile tiers** — Low / Mid / High drive FPS + VFX via `MobileQualityRuntime` / `OnQualityChanged` (see [MobileOptimizationChecklist.md](MobileOptimizationChecklist.md)).
8. **No new direct PlayerPrefs** — use `GameServices.Instance.Save` (`ISaveService`). Legacy Currency/GameOver prefs migrate in M4.
9. **GameplayEvents for new content** — subscribe to typed `GameplayEvents` instead of coupling new abilities to managers.
10. **Pool combat FX** — no repeated Instantiate/Destroy in combat; use `PoolService` / specialized pools ([PoolingRules.md](PoolingRules.md)). Enemy pooling deferred to M3.
11. **Addressables Load → Use → Release** for heavy content ([AddressablesFoundation.md](AddressablesFoundation.md)).
12. **Validate before Play** — `Game → Foundation → Validate Game Content`.

## Config access

```
GameServices.Instance.Config   // GameConfigRegistry
  ├── SceneFlow
  ├── GameBalance
  ├── MobileQuality
  ├── ActiveAbilities
  └── DefaultWaveTable
```

Edit balance only at `Assets/Content/Balance/GameBalanceConfig.asset`.  
Flow: **GameConfigRegistry → GameBalanceConfig → ManaManager** (not Resources/GameBalanceConfig).

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

Typed publish-only bus: `GameplayEvents` — BattleStarted/Ended (phase-change only), WaveStarted, Enemy*, UnitSummoned / UnitMerged / UnitUpgraded / UnitTransformed, `GameplayDamageEvent`, BossSpawned, StatusApplied (slow/poison/stun).

## Folder map

```
Assets/
  Content/
    Resources/GameConfigRegistry.asset
    Config/, Balance/, Quality/, Abilities/, Enemies/, Waves/, Units/
  GUI/                                 ← client screen pack (M1 uses Gameplay_HUD only)
  Resources/                           ← audio/VFX helpers only
  Script/Core/
  Scenes/Bootstrap.unity
```

## Content change workflow (no code)

| Change | Edit this (one place) |
|--------|------------------------|
| Mana / summon cost | `Content/Balance/GameBalanceConfig` |
| Active abilities | `Content/Abilities/*` via catalog |
| Quality FPS/VFX | `Content/Quality/MobileQuality_*` |
| Scene names | `Content/Config/SceneFlowConfig` |
| Registry wiring | `Content/Resources/GameConfigRegistry` |

## Explicitly deferred

- Filling 6 global actives (M2)
- Main Menu / Deck / Victory GUI from `Assets/GUI` (M2/M4)
- Enemy pooling + EnemyDefinition/WaveTable live spawn migration (M3)
- Enemy behavior components / full progression save JSON (M3–M4)
- Migrating CurrencyManager Gold/Gems off PlayerPrefs (M4)
- Remote Addressables CDN

## Acceptance (architecture)

- [x] Single GameConfigRegistry under Content/Resources
- [x] Summon cost owned by ManaManager; resets on BeginBattle
- [x] ISaveService for new persistence
- [x] SceneFlow public API simplified
- [x] GameplayEvents hardened (battle/damage/status/merge semantics)
- [x] MobileQuality wired + OnQualityChanged
- [x] PoolService double-release protection
- [x] Local Addressables helper + groups folder (empty groups OK)
- [x] Game Content validator menu
- [ ] Arena / Battle HUD from `Assets/GUI` Gameplay_HUD (Days 2–3)
- [ ] Per-milestone changelog + Android APK (Day 5 + developer)

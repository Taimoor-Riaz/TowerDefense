# Milestone 1 — Known Issues & Scope Honesty

These are **expected** at M1 exit / post-M1 hardening. They are not defects against foundation scope.

## By design / deferred

| Item | Status | When |
|------|--------|------|
| Global active cast + pre-match ability picker | Not implemented | Client M2 |
| Ability SO `implemented = false` | Stubs only | M2 |
| Enemy behavior components / GDD elites-bosses | Prefabs partial; SO stubs | M3 |
| WaveTable not driving WaveBossManager yet | Stub asset exists | M2/M3 |
| Progression (Cards/Runes/leagues) + full save JSON | Missing | M4 |
| Shop/Gift/Quest/Event real logic | UI shells / fake | Later |
| Prefab folder renames (Zeus→Thunder Oracle, etc.) | NamingMap aliases OK | Polish |
| CurrencyManager Gold/Gems still on PlayerPrefs | Legacy; new code uses ISaveService | M4 |

## Technical notes (updated)

| Item | Notes |
|------|-------|
| Config source of truth | **Only** `Assets/Content/Resources/GameConfigRegistry.asset` → Content configs |
| Economy flow | GameConfigRegistry → GameBalanceConfig → ManaManager (`ResetMatchEconomy` on BeginBattle) |
| Summon cost | Battle-only on **ManaManager**; not in CurrencyManager / PlayerPrefs |
| Enemy pooling | Deferred to **M3** with WaveTable/EnemyDefinition migration |
| Additive Hub+Battle | Hub unloads during battle |
| Prototype ability binding | Disabled (`enablePrototypeTowerBinding = false`) |
| SceneFlow missing in player build | Logs critical error (editor may fallback) |
| Addressables | Helper + menu ready; commit `AddressableAssetsData` after first Initialize |
| APK artifact | Build locally — see Android_BuildSmoke.md (physical device) |

## Do not claim as “done” in client demos

- Ranked / opponent comparison  
- Full economy upgrades  
- “All 8 global abilities playable”  
- Enemy specials as designed in GDD  

# Milestone 1 — Known Issues & Scope Honesty

These are **expected** at M1 exit. They are not defects against foundation scope.

## By design / deferred

| Item | Status | When |
|------|--------|------|
| Global active cast + pre-match 2-ability picker | Not implemented | M2 |
| Ability SO `implemented = false` | Stubs only | M2 |
| Main Menu / Deck / Victory-Defeat **new GUI pack** | In `Assets/GUI` but not wired | M2 (Deck) / M4 (hub + V/D) |
| Enemy behavior / GDD elites-bosses | Prefabs partial; SO stubs | M3 |
| WaveTable not driving WaveBossManager | Stub asset exists | M3 |
| Enemy pooling | Instantiate/Destroy remains | M3 |
| Progression (Cards/Runes/leagues) + full save JSON | Missing | M4 |
| Shop/Gift/Quest/Event real logic | UI shells / fake | M4+ |
| iOS | Not in MVP | After M5 unless agreed |
| Prefab folder renames (Zeus→Thunder Oracle) | NamingMap aliases | Polish |
| CurrencyManager Gold/Gems still on PlayerPrefs | Legacy; new code uses ISaveService | M4 |

## Technical notes

| Item | Notes |
|------|-------|
| Config | Only `Assets/Content/Resources/GameConfigRegistry.asset` |
| Economy | Registry → GameBalanceConfig → ManaManager (`ResetMatchEconomy` on BeginBattle) |
| GUI Day 1 | Pack imported as Sprite (2D/UI). Battle still uses **old** arena/HUD until Days 2–3 |
| Addressables | `Assets/AddressableAssetsData/` groups exist and are empty (OK) |
| Min SDK | Player Settings **25** (docs aligned) |
| APK | Not in git (`*.apk` gitignored). Deliver `Build_Apk/MergeDefense_M1.apk` beside source |

## Do not claim as done in client demos

- Ranked / opponent comparison  
- Full economy upgrades  
- “All 8 global abilities playable”  
- Enemy specials as designed in GDD  
- New Main Menu / Deck Builder / Victory screens from the GUI pack  

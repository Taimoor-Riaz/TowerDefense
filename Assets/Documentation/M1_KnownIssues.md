# Milestone 1 — Known Issues & Scope Honesty

These are **expected** at M1 exit. They are not defects against foundation scope.

## By design / deferred

| Item | Status | When |
|------|--------|------|
| Global active cast + pre-match ability picker | Not implemented | Client M2 |
| Ability SO `implemented = false` | Stubs only | M2 |
| Enemy behavior components / GDD elites-bosses | Prefabs partial; SO stubs | M3 |
| WaveTable not driving WaveBossManager yet | Stub asset exists | M2/M3 |
| Progression (Cards/Runes/leagues) | Missing | M4 |
| Shop/Gift/Quest/Event real logic | UI shells / fake | Out of M1; later |
| Prefab folder renames (Zeus→Thunder Oracle, etc.) | NamingMap aliases OK | Polish |
| `Shapeshifte_Data` filename typo | Tracked in NamingMap | Polish |
| Gravity Well / Barrier Pulse | Stretch | After 6 launch actives |

## Technical notes

| Item | Notes |
|------|-------|
| Content vs Resources GameBalanceConfig | Runtime reads **Resources**; keep Content copy synced |
| Additive Hub+Battle | Hub unloads during battle to avoid dual EventSystem |
| Prototype ability binding | Disabled; re-enable only for demos via inspector flag |
| APK artifact | Build locally — see Android_BuildSmoke.md |

## Do not claim as “done” in client demos

- Ranked / opponent comparison  
- Full economy upgrades  
- “All 8 global abilities playable”  
- Enemy specials as designed in GDD  

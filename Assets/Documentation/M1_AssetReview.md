# Milestone 1 — Asset Review (Day 1)

**Pack:** `Assets/GUI/Screens - Main Menu, Battle HUD, Editor/`  
**Import check:** all sampled textures are Unity `textureType: 8` (**Sprite (2D and UI)**).  
**Day 1 prep:** `PNG/Gameplay_HUD/**` sprite mode is **Multiple** (one slice, same as `Map__Fire`) so Battle Images can bind the named sprite IDs. Other GUI folders stay as imported until M2/M4.

JPG mocks (reference only): `Gamplay_HUD.jpg`, `Main Menu Screen.jpg`, `Deck Building.jpg`, `Victory Screen.jpg`, `Defeat Screen.jpg`.  
Figma source: `FIGMA/Merge Tower.fig` (not used at runtime).

## M1 — use these (Gameplay HUD + Arena)

Mock: `JPG/Gamplay_HUD.jpg`

### Arena (Day 2)

| File | Role |
|------|------|
| `PNG/Gameplay_HUD/GamePlay_BG.png` | Full-screen battle background |
| `PNG/Gameplay_HUD/Arena/Areena_BG.png` | Arena board |
| `PNG/Gameplay_HUD/Arena/Grid_BG.png` | Grid fill |
| `PNG/Gameplay_HUD/Arena/Grid_Frame.png` | Grid frame |
| `PNG/Gameplay_HUD/Arena/Grid_Frame_Line.png` | Lane / grid lines |
| `PNG/Gameplay_HUD/Arena/Grid_Frame_Circle.png` | Cell circles |
| `PNG/Gameplay_HUD/Arena/Gird_Box.png` | Grid box (filename typo) |
| `PNG/Gameplay_HUD/Arena/Wave Line.png` | Path / wave line |
| `PNG/Gameplay_HUD/Arena/Border wal  3.png` | Border wall (filename typo) |

### Header (Day 3)

| File | Role |
|------|------|
| `PNG/Gameplay_HUD/Header/Footer_BG.png` | Header bar background |
| `PNG/Gameplay_HUD/Header/Opponent Name Frame.png` | Opponent name chrome (placeholder data OK) |
| `PNG/Gameplay_HUD/Header/Oppnent_Unit Frame.png` | Opponent unit frame |
| `PNG/Gameplay_HUD/Header/Oppnent_Unit Icon.png` | Opponent unit icon slot |
| `PNG/Gameplay_HUD/Header/Oppnent_Unit Badge.png` | Badge |
| `PNG/Gameplay_HUD/Header/Opponent_Abilities_Icon.png` | Opponent abilities chrome |

### Footer (Day 3) — keep existing button wiring

| File | Role |
|------|------|
| `PNG/Gameplay_HUD/Footer/Footer_BG.png` | Footer bar |
| `PNG/Gameplay_HUD/Footer/Summon_Button.png` | Summon button |
| `PNG/Gameplay_HUD/Footer/Summont_Icon.png` | Summon icon (filename typo) |
| `PNG/Gameplay_HUD/Footer/Mana_BG.png` | Mana frame |
| `PNG/Gameplay_HUD/Footer/Mana_Icon.png` | Mana icon |
| `PNG/Gameplay_HUD/Footer/Abbilities_Frame.png` | Ability slot frame |
| `PNG/Gameplay_HUD/Footer/Abilities.png` / `Abilities_Icon.png` | Ability chrome |
| `PNG/Gameplay_HUD/Footer/Abbilities_Icon Tag.png` | Ability tag |
| `PNG/Gameplay_HUD/Footer/Unit_Frame.png` / `Unit_Icon.png` / `Unit_Tag.png` | Selected-unit chrome (display only; not product ability bind) |

**Rule:** Ability / Ability_2 stay **unbound** (`enablePrototypeTowerBinding = false`).

## Not M1 — leave in folder

| Folder | Count (PNG) | When |
|--------|-------------|------|
| `PNG/Main Menu/` | 66 | M4 hub reskin |
| `PNG/Deck Building/` | 34 | M2 loadout (6 units + 2 actives UI) |
| `PNG/Victory_Defeat Screen/` | 28 | M4 Game Over / victory |

Current scenes: **Day 4** Battle footer under `BattleSafeAreaRoot`; Game Over under `GameOverSafeAreaRoot`. Arena / lanes remain full-bleed on `Map_Bg`. Spawn / exit follow route `Rp` Transforms.

## Still placeholder / later (not this pack)

- Ability **icons** for Meteor Strike etc. (M2)  
- Unit portrait/prefab folder names Zeus/Princess (`NamingMap.md`)  
- Hub Shop/Guild/Rank **systems** (shells only)

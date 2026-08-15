# Milestone Crosswalk

Maps **client milestones** to audit phases and GDD §19.

**Platform:** Android is the MVP. iOS is a later phase and is **not** in M1–M5 acceptance.

| Client milestone | Days | Focus |
|------------------|------|--------|
| **M1 Foundation & Technical Setup** | 10 | Cleanup, architecture, Arena/HUD from `Assets/GUI` Gameplay_HUD, board/spawn/safe area, asset review, Android test build, source + changelog + QA + known issues |
| **M2 Core Gameplay Systems** | 15 | 6-unit deck + **2 global actives**, Option A cast, unit data, status, save/load, APK + docs |
| **M3 Combat, Enemy & Boss** | 15 | Enemy AI, bosses, waves, combat balance, APK + docs |
| **M4 Progression, Meta & UI** | 10 | Economy/unlocks, **final** Main Menu / Deck / Victory-Defeat GUI, layout, APK + docs |
| **M5 Final QA & MVP Candidate** | 10 (20 revisions) | QA, bugs, performance, Android, MVP candidate, final source + docs |

## Per-milestone delivery (every accepted milestone)

1. Complete Unity project source  
2. Android APK/test build where applicable  
3. Short changelog  
4. QA checklist  
5. Known issues list  

## Milestone is complete only after

- Deliverables above exist  
- Scoped feature is visible and testable in-game  
- Android build works on a physical device (not auto-passed)  
- No critical/blocker bugs unless waived in writing  
- Client confirms acceptance in writing  

## Option A (product direction, this order)

1. Pre-match selection of **6 units**  
2. Pre-match selection of **2 global active abilities**  
3. Ability buttons **independent** of selected tower/unit  
4. Unit abilities remain **passive / identity** skills  
5. Tower-selected ability UX is **not** the product path  

## Graphic assets

Client pack: `Assets/GUI/Screens - Main Menu, Battle HUD, Editor/`

| Pack folder | When |
|-------------|------|
| PNG/Gameplay_HUD + Arena | **M1** (Days 2–4 remaining) |
| PNG/Deck Building | M2 loadout UI |
| PNG/Main Menu, Victory_Defeat | M4 full UI integration |

## GDD §19 buckets

- **§19.1 Core Combat** — board/summon/merge/mana/waves — M1–M2  
- **§19.2 Unit Content** — identity passives — M2–M3  
- **§19.3 Progression** — economy/upgrades/leagues — M4  
- **§19.4 Competitive** — replay opponent — M4  

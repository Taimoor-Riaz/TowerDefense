# Milestone Crosswalk

Maps **client milestones** to audit phases and GDD §19.

| Client milestone | Audit / Phase | Focus |
|------------------|---------------|--------|
| **M1 Foundation & Technical Setup** (10 working days) | M0 / Phase 1 / Repair §4.1 | Config-first architecture, additive scenes, mobile quality baseline, Option A lock, naming map, Android smoke |
| **M2 Core Gameplay Systems** | Audit M1 Ability + harden core | Pre-match 6 units + 2 actives, ActiveAbility SO catalog, global cast, HUD unbound from tower |
| **M3 Combat, Enemy & Boss** | Audit M2 (+ unit identity pass) | EnemyDefinition, 6+3+3 mechanics, wave tables |
| **M4 Progression, Meta & UI** | Audit M4–M5 | Gold/Gems/Cards/Runes, save, leagues/opponent v1, UI wiring |
| **M5 Final QA & MVP Candidate** | Audit M6 | Polish, crash pass, Android MVP candidate |

## GDD §19 buckets

- **§19.1 Core Combat** — board/summon/merge/mana/waves — mostly exist; harden under M1–M2 architecture
- **§19.2 Unit Content** — 11 units Skill-1 identity — improve M3
- **§19.3 Progression** — economy/upgrades/leagues — M4
- **§19.4 Competitive** — replay opponent — M4

## Product lock

**Option A:** pre-match **6 units + 2 global actives**; unit abilities are **passives**; do not expand tower-selected ability UX.

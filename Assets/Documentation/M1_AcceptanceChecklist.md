# Milestone 1 — Acceptance Checklist

**Milestone:** Client M1 — Project Foundation & Technical Setup (10 working days)  
**Platform:** Android portrait only (iOS not in MVP)  
**Remaining close-out:** 5 working days (architecture already in repo)

## Acceptance gate (every milestone)

| # | Criterion | Status | Evidence |
|---|-----------|--------|----------|
| 1 | Deliverables provided (source, changelog, QA, known issues, APK where applicable) | [ ] | Changelog in repo; APK by developer |
| 2 | Scoped feature visible and testable in-game | [ ] | Bootstrap play + Arena/HUD after Days 2–4 |
| 3 | Android build works on a physical device | [ ] | Android_BuildSmoke — **not auto-passed** |
| 4 | No critical/blocker unless waived in writing | [ ] | KnownIssues |
| 5 | Client written acceptance | [ ] | This sheet |

## M1-specific

| # | Criterion | Status | Evidence |
|---|-----------|--------|----------|
| A | Config-first architecture + Content folders | [x] | ScalableArchitecture.md, Assets/Content/ |
| B | Bootstrap + additive SceneFlow | [x] | Bootstrap.unity, SceneFlowService |
| C | Naming map delivered | [x] | NamingMap.md — **client sign-off [ ]** |
| D | Option A documented (6 units + 2 actives; buttons unbound; passives; no tower-select path) | [x] | OptionA_AbilityArchitecture.md |
| E | Tower-selected ability path quarantined | [x] | `enablePrototypeTowerBinding = false` |
| F | Mana / summon cost from GameBalanceConfig | [x] | Registry → ManaManager |
| G | Water ≠ in-match mana | [x] | ManaManager isolation |
| H | Mobile Low/Mid/High | [x] | MobileQualityService |
| I | Android Player Settings | [x] | `com.competitivesurvival.mergedefense`, min **25**, target 34 |
| J | Risk register + M2 backlog | [x] | RiskRegister_M1.md, M2_Backlog_AbilityCorrection.md |
| K | README open/build/scenes | [x] | Root README.md |
| L | Single GameConfigRegistry | [x] | `Assets/Content/Resources/GameConfigRegistry.asset` |
| M | Validate Game Content menu | [x] | Game → Foundation → Validate Game Content |
| N | Addressables local groups exist | [x] | `Assets/AddressableAssetsData/` (groups empty until M2/M3) |
| O | Client GUI pack imported (Sprite 2D/UI) | [x] | Day 1 — `Assets/GUI/` inventory |
| P | Arena / Battle HUD from Gameplay_HUD pack | [x] | Day 2 arena + Day 3 HUD chrome; Ability still unbound |
| Q | Board / spawn / end + safe area on new arena | [x] | Day 4 — route Transforms + BattleSafeAreaRoot |
| R | Per-milestone pack: changelog + QA + known issues | [x] | Day 5 — root `CHANGELOG.md` + M1 docs |
| S | Android APK test build | [ ] | Developer: `Build_Apk/MergeDefense_M1.apk` |

## Sign-off

| Role | Name | Date | Sign |
|------|------|------|------|
| Engineering | | | [ ] |
| QA | | | [ ] |
| Client | | | [ ] |

**M1 closed when:** A–S as applicable, Android smoke (row 3) done, no open Critical without written waiver, client written acceptance.

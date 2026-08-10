# Milestone 1 — Acceptance Checklist

**Milestone:** Client M1 — Project Foundation & Technical Setup (10 working days)  
**Architecture map:** Audit M0 / Phase 1 / Repair §4.1  

## Standard gate (§8)

| # | Criterion | Status | Evidence |
|---|-----------|--------|----------|
| 1 | Project opens/builds without blocking errors | [ ] Eng | Unity 6000.3.10f1 open + compile |
| 2 | Core loop not regressed (summon/merge/combat/leak) | [ ] QA | Editor smoke (Bootstrap → battle → game over) |
| 3 | Scope honesty — stubs not presented as finished features | [x] | KnownIssues + docs |
| 4 | Android verified on ≥1 device/profile | [ ] QA | Android_BuildSmoke (physical device — not auto-passed) |
| 5 | QA checklist signed | [ ] QA/Client | This sheet |

## M1-specific (foundation)

| # | Criterion | Status | Evidence |
|---|-----------|--------|----------|
| A | Config-first architecture doc + Content folders | [x] | ScalableArchitecture.md, Assets/Content/ |
| B | Bootstrap + additive SceneFlow on product path | [x] | Bootstrap.unity, SceneFlowService, MainMenuUI/GameOverUI |
| C | Naming map delivered | [x] | NamingMap.md — **client sign-off [ ]** |
| D | Option A design note + ability inventory | [x] | OptionA_AbilityArchitecture.md, AbilityInventory.md |
| E | Tower-selected ability path quarantined | [x] | `enablePrototypeTowerBinding = false` |
| F | Live tunable via SO (mana/summon cost) | [x] | GameConfigRegistry → GameBalanceConfig → ManaManager |
| G | Water ≠ in-match mana | [x] | ManaManager isolation + CurrencyManager docs |
| H | Mobile Low/Mid/High quality applies | [x] | MobileQualityService + profiles + consumers |
| I | Android Player Settings baseline | [x] | package `com.competitivesurvival.mergedefense`, min 24, target 34 |
| J | Risk register + M2 backlog delivered | [x] | RiskRegister_M1.md, M2_Backlog_AbilityCorrection.md |
| K | README documents open/build/scenes | [x] | Root README.md |
| L | Single GameConfigRegistry | [x] | `Assets/Content/Resources/GameConfigRegistry.asset` only |
| M | Content validator menu | [x] | Game → Foundation → Validate Game Content |
| N | Addressables foundation (local groups) | [ ] Eng | Run Initialize Addressables Groups once; commit AddressableAssetsData |

## Sign-off

| Role | Name | Date | Sign |
|------|------|------|------|
| Engineering | | | [ ] |
| QA | | | [ ] |
| Client / Design (NamingMap) | | | [ ] |

**M1 closed when:** A–N checked, Android smoke (row 4) done, NamingMap signed, no open Critical without waiver.

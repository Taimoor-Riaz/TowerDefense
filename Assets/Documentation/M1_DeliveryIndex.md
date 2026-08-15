# Milestone 1 — Delivery Pack Index

**Product:** Competitive Survival Merge Defense  
**Milestone:** Foundation & Technical Setup (10 working days)  
**Engine:** Unity 6000.3.10f1 · URP 2D · Android portrait  

## What was delivered

1. **Scalable / config-first architecture** — content & balance in ScriptableObjects under `Assets/Content/`  
2. **Single GameConfigRegistry** — `Assets/Content/Resources/GameConfigRegistry.asset`  
3. **Additive scene loading** — `Bootstrap` → Hub / Battle via `SceneFlowService`  
4. **Option A lock** — naming map, ability inventory, tower-ability UX quarantined  
5. **GameBalanceConfig** via registry → `ManaManager` (summon cost resets per battle)  
6. **Mobile quality** Low / Mid / High + Android Player Settings baseline  
7. **GameplayEvents** hardened for M2  
8. **PoolService** + pooling rules; Addressables foundation helper  
9. **Validate Game Content** menu  
10. **Planning** — risk register + Milestone 2 ability backlog  

## Document checklist (hand to client)

| Doc | Path |
|-----|------|
| Delivery index (this file) | `Assets/Documentation/M1_DeliveryIndex.md` |
| Acceptance checklist | `M1_AcceptanceChecklist.md` |
| Known issues | `M1_KnownIssues.md` |
| Risk register | `RiskRegister_M1.md` |
| M2 backlog | `M2_Backlog_AbilityCorrection.md` |
| Architecture rules | `ScalableArchitecture.md` |
| Milestone crosswalk | `MilestoneCrosswalk.md` |
| Naming map | `NamingMap.md` |
| Option A | `OptionA_AbilityArchitecture.md` |
| Ability inventory | `AbilityInventory.md` |
| Pooling rules | `PoolingRules.md` |
| Addressables foundation | `AddressablesFoundation.md` |
| Mobile checklist | `MobileOptimizationChecklist.md` |
| Android smoke | `Android_BuildSmoke.md` |
| GUI asset review (Day 1) | `M1_AssetReview.md` |
| Final QA checklist | `M1_FinalQAChecklist.md` |
| Root README | `/README.md` |

## Remaining close-out (5 days)

| Day | Status |
|-----|--------|
| 1 Contract docs + GUI import | **Done** — no Battle HUD swap |
| 2 Arena sprites | **Done** — HUD buttons still old |
| 3 Battle HUD sprites | **Done** — summon/ability wiring unchanged |
| 4 Safe area + board | **Done** — `BattleSafeAreaRoot` / `GameOverSafeAreaRoot`; spawn/exit use route Transforms |
| 5 Changelog + APK (developer) | **Docs done** — changelog in repo root; APK + device smoke remain developer-owned |

## Final cleanup (pre-M2)

- Single registry under `Content/Resources`
- GameplayEvents: phase-guarded battle events, `GameplayDamageEvent`, status + merge/upgrade/transform
- PoolService double-release guard
- Enemy pooling deferred to M3 (documented)
- Content validator enforces one registry + full refs

## Build artifact

- **APK:** Produce via Unity Android build → recommend `Build_Apk/MergeDefense_M1.apk`  
- Attach device log + short video of Bootstrap → match → Hub if required by contract  
- **Android smoke is not auto-passed** — requires physical device verification  

## Next milestone

Start **[M2_Backlog_AbilityCorrection.md](M2_Backlog_AbilityCorrection.md)** only after AddressableAssetsData is generated/committed and Editor smoke passes.

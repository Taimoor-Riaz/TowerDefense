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
| Root README | `/README.md` |

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

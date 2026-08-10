# Milestone 1 — Delivery Pack Index

**Product:** Competitive Survival Merge Defense  
**Milestone:** Foundation & Technical Setup (Days 1–5)  
**Engine:** Unity 6000.3.10f1 · URP 2D · Android portrait  

## What was delivered

1. **Scalable / config-first architecture** — content & balance in ScriptableObjects under `Assets/Content/`  
2. **Additive scene loading** — `Bootstrap` → Hub / Battle via `SceneFlowService`  
3. **Option A lock** — naming map, ability inventory, tower-ability UX quarantined  
4. **GameBalanceConfig** wired to match mana / summon cost  
5. **Mobile quality** Low / Mid / High + Android Player Settings baseline  
6. **Planning** — risk register + Milestone 2 ability backlog  

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
| Day 3 smoke | `Day3_SmokeChecklist.md` |
| Mobile checklist | `MobileOptimizationChecklist.md` |
| Pooling rules (M2 prereq) | `PoolingRules.md` |
| Addressables foundation | `AddressablesFoundation.md` |
| Android smoke | `Android_BuildSmoke.md` |
| Root README | `/README.md` |

## Priority 2 (pre-M2) — done

- MobileQuality consumers + `OnQualityChanged`
- `PoolService` + pooling rules
- Local Addressables groups + `AddressableContent`
- Menu: **Game → Foundation → Validate Game Content**

## Build artifact

- **APK:** Produce via Unity Android build → recommend `Build_Apk/MergeDefense_M1.apk`  
- Attach device log + short video of Bootstrap → match → Hub if required by contract  

## Next milestone

Start **[M2_Backlog_AbilityCorrection.md](M2_Backlog_AbilityCorrection.md)** — 6 global actives + pre-match loadout + HUD unbound from towers.

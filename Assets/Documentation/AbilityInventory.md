# Ability Inventory — Passive vs Prototype-Button

**Date:** Day 2  
**Rule:** Keep passives. Do not expand prototype-button path. Globals come from `ActiveAbilityDefinition` (M2).

Classification:

- **Passive** — auto / on-hit / interval / drag; OK for Option A identity  
- **Prototype-button** — `SupportsManualActivation` and can bind via `HeroAbilityButtonController` when a tower is selected; **non-product** for global HUD  
- **Board utility** — drag interaction; keep

| Script | AbilityName | SupportsManualActivation | Classification | Unit (project) | Notes |
|--------|-------------|----------------------------|----------------|----------------|-------|
| `FireMageAoEAbility` | Fire Explosion | true (default) | Passive primary + Prototype-button eligible | Fire Mage | On-hit AoE is identity; manual path is prototype |
| `FrostWitchSlowAbility` | Frost Slow | true | Passive primary + Prototype-button eligible | Frost Witch | On-hit slow = Skill 1; ≠ Frost Nova global |
| `GoldSpiritAbility` | Mana Generation | **false** | Passive | Golden Spirit | Interval mana — keep |
| `ShapeshifterAbility` | Shapeshift | **false** | Board utility | Shapeshifter | Drag copy — keep |
| `ChainLightningAbility` | Chain Lightning | true | Passive primary + Prototype-button eligible | Zeus / Thunder Oracle | On-hit chain — keep as passive |
| `StoneGolemStunAbility` | Stone Guardian Stun | true | Passive primary + Prototype-button eligible | Stone Guardian | On-hit stun — keep |
| `NatureBlessingBuffAbility` | Nature Blessing | true | Passive/aura + Prototype-button eligible | Enchantress | Ally buff — keep as identity |
| `PlagueDoctorPoisonAbility` | Plague Poison | true | Passive primary + Prototype-button eligible | Poison Druid | On-hit DoT — keep |
| `LightFairyAbility` | Radiant Blessing | **false** | Board utility | Light Fairy | Drag upgrade — keep |
| `HeroAbilityButtonController` | (controller) | n/a | **Prototype-only / quarantine** | — | Day 3: disable product binding |
| `TowerAbilityBase` | (base) | default true | Refactor later | — | Split passive vs global active runtime in M2 |

## Global actives (not implemented as product yet)

All eight GDD globals are **Missing** as loadout actives. Stub SO assets exist under `Assets/Content/Abilities/` for the six launch abilities.

## Day 3 quarantine intent

1. Stop `HeroAbilityButtonController` from binding HUD to selected tower (or gate behind `#if` / inspector flag `enablePrototypeTowerBinding = false`).  
2. Leave unit passive scripts running.  
3. HUD buttons remain for M2 global cast wiring.

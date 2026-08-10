# Client Milestone 2 Backlog — Ability System Correction

**Depends on:** Milestone 1 foundation (done) + Priority 2 (quality consumers, pooling, Addressables, content validator)  
**Maps to:** Audit M1 / Phase 2 — Ability system correction  
**Rule:** All new content goes into `Assets/Content/` ScriptableObjects. No new magic numbers in cast code.  
**M2 hard rules:** Spawn ability/impact VFX via `AbilityVfxPool` or `PoolService` only. Heavy assets via Addressables Load→Release. Respect `MobileQualityRuntime`.

## Goal

Pre-match: select **6 units + 2 global actives**.  
In-match: Ability / Ability_2 cast those actives (not selected tower).  
Ship **6 launch actives** with v1 values.

## Launch pool (must)

1. Meteor Strike  
2. Frost Nova  
3. Mana Surge  
4. Radiant Cleanse  
5. Arcane Overclock  
6. Execution Sigil  

Stretch (only if ahead): Gravity Well, Barrier Pulse.

## Work packages

### WP1 — Data & loadout model
- [ ] Match loadout runtime object: 6× `UnitData` + 2× `ActiveAbilityDefinition`
- [ ] Persist selected actives for the match via `ISaveService` / `SaveKeys` (not raw PlayerPrefs)
- [ ] Subscribe new actives/passives to `GameplayEvents` where appropriate
- [ ] Read ability/balance data from `GameServices.Instance.Config`
- [ ] Mark catalog entries; set `implemented = true` as each ability ships

### WP2 — Pre-match UI
- [ ] Extend deck flow (`DeckSelectionManager`) with 2-ability picker
- [ ] Read pool from `ActiveAbilityCatalog`
- [ ] Block start until 6 units + 2 actives chosen

### WP3 — Global cast service
- [ ] `GlobalActiveCastService` (or under `GameServices`) — cooldown per slot, targeting
- [ ] Rebind HUD Ability / Ability_2 to loadout slots (replace prototype path)
- [ ] Keep `enablePrototypeTowerBinding = false`; remove dependence on selected tower
- [ ] Unit passives continue via existing scripts

### WP4 — Implement 6 actives (read SO power/radius/cooldown)
- [ ] Meteor Strike — readable AoE damage (pooled VFX)  
- [ ] Frost Nova — AoE slow (pooled VFX)  
- [ ] Mana Surge — grant in-match mana (`ManaManager`)  
- [ ] Radiant Cleanse — clear/debuff support (v1 simplified OK)  
- [ ] Arcane Overclock — tower AS/damage buff window  
- [ ] Execution Sigil — boss/single-target amp  
- [ ] No combat hot-path Instantiate/Destroy for ability presentation  

### WP5 — QA gate
- [ ] Ability checklist (cast, cooldown UI, no tower select required)
- [ ] Android build regression
- [ ] No regression: summon, merge, leak-fail

## Explicitly out of M2

- Full ability upgrade tree UI  
- Enemy specials / bosses (M3)  
- Progression cards/runes/leagues (M4)  
- Expanding tower-selected ability UX  

## Acceptance (audit-aligned)

- Feature selectable in normal pre-match UI  
- Both actives visible, cooldown-readable, testable in battle  
- Casting does not require selecting a tower  
- Works on Android  
- QA checklist signed  

## Starting files

| Concern | Path |
|---------|------|
| Ability stubs | `Assets/Content/Abilities/` |
| Catalog | `ActiveAbilityCatalog.asset` |
| HUD quarantine | `HeroAbilityButtonController.cs` |
| Deck | `Assets/Script/Deck/DeckSelectionManager.cs` |
| Option A rules | `OptionA_AbilityArchitecture.md` |
| Naming | `NamingMap.md` |

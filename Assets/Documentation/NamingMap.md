# Naming Map (GDD ↔ Project)

**Status:** Day 2 freeze — use these names for all future content wiring  
**Sign-off:** Pending client/design approval (print/sign when agreed)

Display names in UI should prefer **GDD name**. Prefab/folder names may keep current project names until an art rename pass.

---

## Units (11)

| GDD name (canonical) | Project folder / prefab | UnitData asset | `unitName` field today | Role (GDD intent) | Canonical ID |
|----------------------|-------------------------|----------------|------------------------|-------------------|--------------|
| Fire Mage | `Fire Mage/` | `FireMage_Data.asset` | Fire Mage | AoE / wave clear | `unit_fire_mage` |
| Frost Witch | `Frost Witch/` | `Frost Witch_Data.asset` | Frost Witch | Slow / control | `unit_frost_witch` |
| Gold Spirit | `Golden Spirit/` | `Golden Spirit_Data.asset` | Golden Spirit | Mana economy | `unit_gold_spirit` |
| Shapeshifter | `Shapeshifter/` | `Shapeshifte_Data.asset` (typo in filename) | Shapeshifter | Board utility (copy) | `unit_shapeshifter` |
| Thunder Oracle | `Zeus/` | `Zeus_Data.asset` | Zeus | Chain / multi-hit | `unit_thunder_oracle` |
| Stone Guardian | `Stone Guardian/` | `Stone Guardian_Data.asset` | Stone Guardian | Stun / tank pressure | `unit_stone_guardian` |
| Enchanter | `Enchantress/` | `Enchantress_Data.asset` | Enchantress | Ally buff aura | `unit_enchanter` |
| Poison Druid | `Poison Druid/` | `Poison Druid_Data.asset` | Plague Doctor | DoT / poison | `unit_poison_druid` |
| Light Fairy | `Light_Fairy/` | `Light Fairy_Data.asset` | Light Fairy | Board utility (upgrade) | `unit_light_fairy` |
| Shield Priestess | `Princess/` | `Princess_Data.asset` | Princess | Protection / support | `unit_shield_priestess` |
| Shadow Assassin | `Magic Archer/` | `Magic Archer_Data.asset` | Magic Archer | Boss-killer / single-target | `unit_shadow_assassin` |

### Rename debt (do not block M1; track for polish)

- Filename `Shapeshifte_Data.asset` → `Shapeshifter_Data.asset`
- `unitName` Plague Doctor → Poison Druid (or keep display alias)
- Prefab folders Zeus / Princess / Magic Archer / Enchantress / Golden Spirit — optional folder rename to GDD names later

---

## Global active abilities (GDD pool)

| GDD name | Canonical ID | Launch (MVP) | Content asset (Day 2 stub) |
|----------|--------------|--------------|----------------------------|
| Meteor Strike | `active_meteor_strike` | Yes | `Assets/Content/Abilities/Active_MeteorStrike.asset` |
| Frost Nova | `active_frost_nova` | Yes | `Active_FrostNova.asset` |
| Mana Surge | `active_mana_surge` | Yes | `Active_ManaSurge.asset` |
| Radiant Cleanse | `active_radiant_cleanse` | Yes | `Active_RadiantCleanse.asset` |
| Arcane Overclock | `active_arcane_overclock` | Yes | `Active_ArcaneOverclock.asset` |
| Execution Sigil | `active_execution_sigil` | Yes | `Active_ExecutionSigil.asset` |
| Gravity Well | `active_gravity_well` | Stretch | (create when scheduled) |
| Barrier Pulse | `active_barrier_pulse` | Stretch | (create when scheduled) |

These are **not** the same as unit passives (Frost Witch slow ≠ Frost Nova).

---

## Enemies — normals (GDD-aligned)

| GDD role / name | Project prefab | Canonical ID | Mechanic status |
|-----------------|----------------|--------------|-----------------|
| Basic Walker | (generic / crowler) `Enemy_crowler` | `enemy_basic_walker` | Partial |
| Heavy / Tank | `Enemy_Tank` | `enemy_heavy` | Partial (stats) |
| Runner | `Enemy_Runner` | `enemy_runner` | Partial |
| Swarm / Splitter | `Enemy_Splitter` | `enemy_swarm_splitter` | Prefab; behavior incomplete |
| Shielded | `Enemy_Shield` | `enemy_shielded` | Prefab; shield system incomplete |
| Mana Leech | (missing dedicated) | `enemy_mana_leech` | Missing |

---

## Enemies — elites (GDD)

| GDD name | Project map | Canonical ID | Status |
|----------|-------------|--------------|--------|
| Arcane Suppressor | Unmapped | `elite_arcane_suppressor` | Missing |
| Rank Drainer | Unmapped | `elite_rank_drainer` | Missing |
| Warped Herald | Unmapped | `elite_warped_herald` | Missing |

---

## Bosses (GDD ↔ current prefabs)

| GDD boss | Closest project prefab(s) | Canonical ID | Status |
|----------|---------------------------|--------------|--------|
| Colossus | Fortress Juggernaut / Corruption Core Titan | `boss_colossus` | Visual/HP wall; GDD mechanics missing |
| Brood Core | Necromancer Shaman / Goblin King Grukk | `boss_brood_core` | Missing designed adds/summon check |
| Null Priest | Void_Reaper / Cosmic Devourer | `boss_null_priest` | Missing debuff/disable check |

Other boss prefabs (Infernal Knight, Strom Leviathan, etc.) are prototype art — assign to a canonical ID before shipping MVP roster.

---

## Currencies (clarity)

| Concept | GDD | Code today | Rule |
|---------|-----|------------|------|
| In-match mana | Mana | `ManaManager` | Match-only; never call it Water in HUD |
| Soft currency | Gold | `CurrencyManager` Gold | Meta wallet |
| Premium | Gems | Gems | Meta wallet |
| Legacy wallet field | — | Water | Treat as **meta wallet leftover**; do not seed match mana from Water going forward (Day 3 cleanup) |
| Unit Cards | Unit Cards | Missing | M4 |
| Runes | Runes | Missing / display only | M4 |

---

## Approval

| Role | Name | Date | Approved |
|------|------|------|----------|
| Design / Client | | | [ ] |
| Engineering lead | | | [ ] |

# Content Inventory

Updated **Day 2**.

## Units (GDD 11) — see [NamingMap.md](NamingMap.md)

| GDD name | Project | UnitData | Status |
|----------|---------|----------|--------|
| Fire Mage | Fire Mage | FireMage_Data | Mapped |
| Frost Witch | Frost Witch | Frost Witch_Data | Mapped |
| Gold Spirit | Golden Spirit | Golden Spirit_Data | Mapped |
| Shapeshifter | Shapeshifter | Shapeshifte_Data (typo) | Mapped |
| Thunder Oracle | Zeus | Zeus_Data | Mapped |
| Stone Guardian | Stone Guardian | Stone Guardian_Data | Mapped |
| Enchanter | Enchantress | Enchantress_Data | Mapped |
| Poison Druid | Poison Druid | Poison Druid_Data (unitName Plague Doctor) | Mapped |
| Light Fairy | Light_Fairy | Light Fairy_Data | Mapped |
| Shield Priestess | Princess | Princess_Data | Mapped |
| Shadow Assassin | Magic Archer | Magic Archer_Data | Mapped |

## Global actives

| Ability | SO stub | Implemented cast |
|---------|---------|------------------|
| Meteor Strike | `Content/Abilities/Active_MeteorStrike.asset` | No (M2) |
| Frost Nova | Active_FrostNova | No |
| Mana Surge | Active_ManaSurge | No |
| Radiant Cleanse | Active_RadiantCleanse | No |
| Arcane Overclock | Active_ArcaneOverclock | No |
| Execution Sigil | Active_ExecutionSigil | No |
| Gravity Well / Barrier Pulse | Stretch | — |

Catalog: `Content/Abilities/ActiveAbilityCatalog.asset`

## Enemies

Normal stubs under `Content/Enemies/`. Prefab links + behaviors = M2+.

## Foundation checklist

- [x] Day 1: architecture, Bootstrap, SceneFlow, README
- [x] Day 2: NamingMap, Option A note, AbilityInventory, SO templates + example assets
- [x] Day 3: SceneFlow UI wiring, ability quarantine, GameBalanceConfig → ManaManager, Water≠mana ([Day3_SmokeChecklist](Day3_SmokeChecklist.md))
- [x] Day 4: Mobile quality Low/Mid/High + Android Player Settings + smoke docs ([MobileOptimizationChecklist](MobileOptimizationChecklist.md), [Android_BuildSmoke](Android_BuildSmoke.md))
- [x] Day 5: Acceptance pack — [M1_DeliveryIndex](M1_DeliveryIndex.md), checklist, known issues, risks, M2 backlog

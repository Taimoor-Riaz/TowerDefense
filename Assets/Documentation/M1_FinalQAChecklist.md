# Milestone 1 — Final Editor QA Checklist

Cursor cannot Play Mode or Android device-test.

## Day 1 gate (no HUD swap)

- [ ] Unity finished importing `Assets/GUI/`
- [ ] Play **Bootstrap** → Hub → Battle → summon still works (old arena/HUD is expected)

## One-time setup

1. Let scripts recompile after pull.
2. Addressables groups already exist under `Assets/AddressableAssetsData/` (empty is OK).
3. **Game → Foundation → Validate Game Content** — zero errors.
4. **Game → Foundation → Validate Bootstrap Setup** — `Content/Resources` registry.

## Required play flow (start Bootstrap only)

- [ ] Bootstrap → Main Menu  
- [ ] Battle → Start Match  
- [ ] Summon → mana decreases  
- [ ] Summon again → summon cost increases (50 → 60 → 70)  
- [ ] Merge (normal) works  
- [ ] Towers attack / enemies die  
- [ ] Enemy reaches end → Game Over  
- [ ] Retry → battle reload → summon cost back to **50**  
- [ ] Game Over → Exit → Main Menu  
- [ ] No NullReferenceException / MissingReferenceException  
- [ ] No duplicate GameServices / MobileQualityService / EventSystem / AudioListener  
- [ ] No stuck SceneFlow transition  

After Days 2–3 also:

- [ ] New arena art visible  
- [ ] New HUD chrome visible (header frames, footer bar, summon button, ability frames)  
- [ ] Ability / Ability_2 still do **not** bind to selected tower  
- [ ] Mana shows **~130**, not 500000 / 999999  
- [ ] Summon still spends mana and still works  

## Footer mock layout (Play Bootstrap → Battle)

Cursor cannot Play Mode. Shahzad verifies on device / Editor:

- [ ] Bottom bar looks like the mock: **mana left**, yellow **SUMMON** center, **two ability frames** right, **six unit portraits** on the row below  
- [ ] Wave / Timer / Enemy stay on the **top** HUD (mana is no longer up there)  
- [ ] Summon still spends mana; cost **50 → 60** on the second summon  
- [ ] Merge still works  
- [ ] Ability / Ability_2 still do **nothing** (empty chrome, Option A unbound)  
- [ ] Mana still **~130** at match start  
- [ ] Footer_BG does not block summon clicks  

## Economy / isolation

- [ ] Water (CurrencyManager) does not change with battle mana  
- [ ] `ManaManager.CurrentSummonCost` resets on new battle  

## Android (physical device — do not auto-pass)

Follow [Android_BuildSmoke.md](Android_BuildSmoke.md). Leave unchecked until a real device run succeeds.

## Required play flow (start Bootstrap only)

- [ ] Bootstrap → Main Menu  
- [ ] Battle → Start Match  
- [ ] Summon → mana decreases  
- [ ] Summon again → summon cost increases (50 → 60 → 70)  
- [ ] Merge (normal) works  
- [ ] Towers attack / enemies die  
- [ ] Enemy reaches end → Game Over  
- [ ] Retry → battle reload → summon cost back to **50** (not stuck at 70)  
- [ ] Game Over → Exit → Main Menu  
- [ ] No NullReferenceException / MissingReferenceException  
- [ ] No duplicate GameServices / MobileQualityService / EventSystem / AudioListener during Hub↔Battle  
- [ ] No stuck SceneFlow transition  

## Economy / isolation

- [ ] Water (CurrencyManager) does not change with battle mana  
- [ ] `ManaManager.CurrentSummonCost` resets on new battle via `ResetMatchEconomy`  

## Android (physical device — do not auto-pass)

Follow [Android_BuildSmoke.md](Android_BuildSmoke.md). Leave unchecked until a real device run succeeds.

# Milestone 1 — Final Editor QA Checklist

Cursor cannot Play Mode or Android device-test. Run this in Unity before starting M2.

## One-time setup

1. Let scripts recompile after pull.
2. **Game → Foundation → Initialize Addressables Groups** (if `Assets/AddressableAssetsData/` missing).
3. Commit `Assets/AddressableAssetsData/`.
4. **Game → Foundation → Validate Game Content** — zero errors.
5. **Game → Foundation → Validate Bootstrap Setup** — registry path `Content/Resources`, Resources.Load OK.

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

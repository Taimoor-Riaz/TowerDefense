# Day 3 — Editor smoke checklist

Product path: **Bootstrap → Hub → Battle → GameOver → Hub**

## Steps

1. Open `Assets/Scenes/Bootstrap.unity` (or **Game → Foundation → Open Bootstrap Scene**).
2. Press Play.
3. Confirm Hub (`Main_UI`) loads additively.
4. Tap Battle / Play.
5. Confirm Battle loads additively and Hub unloads.
6. Console: `[Option A] HeroAbilityButtonController: prototype tower-binding DISABLED...`
7. Select a tower — Ability / Ability_2 stay unbound (grey / non-interactable).
8. Summon / merge still works; unit **passives** still fire.
9. Change `Assets/Content/Balance/GameBalanceConfig.asset` `startingMana` (and Resources copy if used) → restart battle → mana reflects config.
10. Leak to game over → Exit → Hub reloads; Try Again → battle reloads via SceneFlow.

## Pass criteria

- [ ] No single-scene `LoadScene` on main Hub↔Battle path when SceneFlow exists
- [ ] Tower-selected ability binding off by default
- [ ] Match mana not written to `CurrencyManager.Water` (`isolateManaFromWalletWater`)
- [ ] Core loop: summon, merge, combat, leak-fail

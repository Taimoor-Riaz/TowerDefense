# Hero Abilities Setup

## Combat mechanic heroes

The remaining combat roles use the existing attack-hit pipeline and are wired to every level 1-6 prefab:

- **Frost Witch / Frost Slow** refreshes a 30% slow for 2 seconds, displays the assigned frost icon, and blends the enemy into the combat theme's blue slow tint.
- **Fire Mage / Fire Explosion** deals 10 explosion damage to every target within 1.2 world units of the projectile impact and plays the pooled radius ring, particles, and assigned explosion sprite.
- **Poison Druid / Plague Poison** refreshes one non-stacking poison instance for 4 seconds. It ticks for 5 damage every second and uses poison-typed floating damage text plus the assigned green status icon.
- **Enchantress / Nature Blessing** is the intended Nature Support hero. It refreshes a 1.25x damage buff on allies within 2.5 world units; the buff lasts 5 seconds and displays a bounds-scaled green aura and overhead indicator.

All balance and presentation values remain serialized on their ability components. Multiple poison applications never create additional tick routines, and multiple Enchantress sources use the strongest active damage multiplier rather than multiplying repeatedly.

The hero ability prefabs are already wired to every level 1-6 prefab for Zeus, Golden Spirit, Stone Guardian, and Shapeshifter. To rebuild the projection-sprite assets or repair prefab references, run:

`Tools > Hero Abilities > Rebuild Prefabs And Wire Heroes`

## Projection sprite visuals

The temporary ability visuals use the existing sliced artwork in `Assets/Sprite/Projection_AoE_Stone`:

- Chain Lightning: `stun.png` beam and impact sprites.
- Mana generation: `gold spirit.png` orb and sparkle sprites.
- Shapeshifter copy: `shapeshifter.png` beam, pulse, and copy-icon sprites.

Sprite renderers are preferred over particle materials, avoiding missing-shader pink effects. Character-relative anchors and world-size calculations come from each tower's `SpriteRenderer.bounds`, so the visuals stay aligned and proportionate across differently sized heroes and merge levels 1-6. The serialized relative-size multipliers remain available for art-direction tuning.

## Existing ability buttons

The runtime `HeroAbilityButtonController` reuses the two existing BattleScene images named `Ability` and `Ability_2` without replacing their artwork. Tap a tower on the board to select it; the slots then bind to the first two manually castable `TowerAbilityBase` components on that selected tower. Clicking empty board space clears the selection, and unused slots stay disabled. Passive-only and drag-only mechanics never occupy a button.

Every ability has serialized `Cooldown Duration` and `Start Ready` controls. After a successful cast, the original button art darkens and refills clockwise with the ability color while a centered label shows the remaining seconds. At full charge the button becomes interactable and receives a pulsing, color-matched glow. UI taps are excluded from board selection raycasts, so pressing a button does not deselect the tower.

Manual casts complement the requested automatic/passive combat behavior:

- Frost Slow affects all enemies currently inside the selected Frost Witch's attack range.
- Fire Explosion detonates on the highest-priority enemy in range.
- Plague Poison affects all enemies in the selected Poison Druid's attack range.
- Nature Blessing immediately refreshes nearby ally buffs.
- Gold Spirit is passive-only; the shared manual ability button cannot award an extra off-timer mana tick.
- Chain Lightning casts from the highest-priority enemy in range.
- Shapeshifter copying is drag-only; the shared manual ability button cannot trigger a copy.
- Light Fairy upgrading is drag-only; the shared manual ability button cannot trigger or consume it.

## Projectile presentation

All bullet prefabs are normalized at runtime to a configurable `0.3` world-unit longest edge, so differently sized sprite slices remain equally readable in battle. Projectiles rotate along their travel direction, pooled instances restore their normalized scale when reused, and the Golden Spirit projectile slice uses a centered pivot. The shared projectile prefab also has a valid sprite reference instead of the removed legacy asset.

## Inspector configuration

### ChainLightningAbility (Zeus / Thunder Lord)

- `Chain Radius`: search radius around the last struck enemy.
- `Maximum Chain Count`: total targets including the primary projectile target.
- `Chain Damage Multiplier`: secondary damage relative to the confirmed primary hit.
- `Chain Delay`: delay between visible jumps.
- `Lightning Color`, `Line Width`, `Beam Lifetime`, `Impact Scale`, `Reference Character Size`, and `Visual Scale Multiplier`: visual balance controls.
- `Lightning Renderer Prefab` and `Lightning Impact Prefab`: pooled VFX references.
- `Chain Sound` and volume: per-jump audio.

The normal projectile owns the first hit. Chain Lightning subscribes to `Tower.AttackHit`; it does not replace targeting, projectiles, or primary damage. Existing `Enemy.TakeDamage` feedback supplies the hit flash and damage number on every target.

### GoldSpiritAbility

- `Mana Per Tick` and `Tick Interval`: base passive output.
- `Merge Level Multiplier`: fractional bonus per level above 1. At the default `0.5`, level 2 gives 150%, level 3 gives 200%, through level 6.
- `Maximum Mana`: per-tick mana-pool cap; set to `0` for no cap.
- Orb/text relative offsets and sizes, travel duration, text lifetime, color, sparkle scale, and reference character size are serialized visual controls.
- `Mana Orb Prefab`, `Mana Gain Text Prefab`, and `Sparkle Prefab` all use `AbilityVfxPool`.

Each placed instance keeps its own active-battle timer and grants its configured merge-level amount every five seconds (defaults: +10, +20, +30, +40, +50, +60). Multiple Golden Spirits stack naturally. The timer resets whenever battle simulation or valid board ownership stops, and mana generation is explicitly passive and cannot be copied by Shapeshifter.

### ShapeshifterAbility

- Beam width/duration, pulse scale, reference character size, and effect color are serialized transformation-feedback controls.
- `Copy Beam Prefab`, `Copy Effect Prefab`, and copy audio are shared pooled feedback references.

Dragging a Shapeshifter onto an allied unit of the same merge level replaces only the Shapeshifter in its original cell with the target UnitData's exact same-level prefab. The target instance and target cell are never cleared or modified. Using the exact prefab gives the result the target identity, sprite/animation, combat stats, projectile, targeting, abilities, status-effect configuration, VFX, and merge-level visuals; it also removes all Shapeshifter behavior from the result.

Different levels, another Shapeshifter, inactive or stale board ownership, missing UnitData, and missing exact-level prefabs are rejected before either cell changes. Empty drops and non-board enemies never resolve to a `BoardTower` target and therefore cancel safely in the drag controller.

## Prefab locations

- `Assets/_Prefabs/HeroAbilities/LightningBeam.prefab`
- `Assets/_Prefabs/HeroAbilities/LightningImpact.prefab`
- `Assets/_Prefabs/HeroAbilities/ManaOrb.prefab`
- `Assets/_Prefabs/HeroAbilities/ManaGainText.prefab`
- `Assets/_Prefabs/HeroAbilities/GoldSparkle.prefab`
- `Assets/_Prefabs/HeroAbilities/CopyBeam.prefab`
- `Assets/_Prefabs/HeroAbilities/CopyEffect.prefab`

## Play Mode test checklist

- [ ] Place Zeus near four enemies. Confirm the primary projectile deals normal damage, then the beam jumps to the nearest unvisited enemy at each step and stops at the configured count/radius.
- [ ] Kill or disable a possible chain target mid-chain. Confirm it is skipped and no enemy is struck twice.
- [ ] Confirm every chain target flashes, shows a damage number, and plays pooled impact/spark feedback.
- [ ] Place one Golden Spirit. Confirm mana is added after each configured interval and the orb, `+N Mana` text, sparkle, and sound play.
- [ ] Place two Golden Spirits. Confirm their independent ticks stack.
- [ ] Compare levels 1-6. Confirm the tick amount follows `Mana Per Tick * (1 + (level - 1) * Merge Level Multiplier)` and respects `Maximum Mana`.
- [ ] Drag a Shapeshifter onto an equal-level ally. Confirm the target instance stays unchanged and the Shapeshifter's original cell receives the target's exact same-level unit prefab.
- [ ] Copy a Gold Spirit, Stone Guardian, or Enchantress. Confirm the copied result has the target's passive/active ability and combat configuration and has no `ShapeshifterAbility`.
- [ ] Try a different merge level, another Shapeshifter, an empty cell, and a missing/blocked target. Confirm both original units and cells remain unchanged.
- [ ] Confirm the copy beam and transformation pulse trigger only after a successful copy.
- [ ] Confirm every Princess prefab has no `ShapeshifterAbility` and cannot enter the Shapeshifter copy branch.
- [ ] Confirm no ability effect renders pink and no procedural/random fallback icon appears when the generated prefabs are assigned.
- [ ] Compare small and large hero sprites at levels 1-6. Confirm beams meet character centers, overhead effects remain above the sprite, and orb/icon/pulse sizes remain proportional.
- [ ] Confirm the existing `Ability` and `Ability_2` scene controls remain in place, bind to placed abilities, and pulse their bound tower when pressed.
- [ ] Profile repeated effects. Confirm temporary beams, particles, orbs, and floating mana text return to `Ability VFX Pool` instead of accumulating instantiated objects.
- [ ] Regression-check summon, merge, normal projectiles, enemy targeting, enemy deaths, mana spending, and existing damage feedback.

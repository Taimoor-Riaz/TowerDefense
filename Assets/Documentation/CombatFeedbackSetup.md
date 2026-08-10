# Combat Feedback Setup

The feedback layer is visual-only. It does not calculate or apply damage, poison, stun, critical hits, or AoE targeting.

## Existing hit flow

`Enemy.TakeDamage(amount)` continues to apply normal damage and now automatically shows a pooled number, a pooled hit burst, a brief enemy flash, and the existing smooth health-bar update.

Use the typed overload only when the existing gameplay system has already decided the damage type:

```csharp
enemy.TakeDamage(amount, EnemyDamageType.Critical);
enemy.TakeDamage(poisonTickDamage, EnemyDamageType.Poison);
```

## Status visuals

Call these alongside the existing gameplay status application. These calls only show or refresh feedback:

```csharp
enemy.ShowStatusIndicator(EnemyStatusType.Stun, stunDuration);
enemy.ShowStatusIndicator(EnemyStatusType.Poison, poisonDuration);
```

Reapplying poison refreshes one aura; it never creates duplicate auras. Stun and poison visuals return to their prewarmed pools when they finish.

## AoE impact

After the existing AoE system has selected its targets, play one radius effect at the impact point:

```csharp
AoEImpactController.PlayImpact(hitPosition, radius, AoEVisualType.Fire);
```

Then keep using `TakeDamage` once for every affected enemy. Each target automatically flashes and receives its own number. `Nature` and `Ice` are available alongside `Fire`.

## Assets and tuning

The five runtime-loaded prefabs are in `Assets/Resources/CombatFeedback`:

- `FloatingDamageText.prefab`
- `StunEffect.prefab`
- `PoisonAura.prefab`
- `AoERadiusRing.prefab`
- `AoEImpactEffect.prefab`

Use **Tools > Combat Feedback > Rebuild All Assets** to regenerate them from the existing game art and theme. Runtime pools prewarm before the battle scene, reuse objects, and recycle the oldest number/impact only if a pool is saturated.

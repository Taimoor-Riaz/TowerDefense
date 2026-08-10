# Pooling Rules (M2 prerequisite)

## Hard rule for combat

**Do not** `Instantiate` / `Destroy` repeatedly during combat for:

- Ability VFX  
- Projectile / impact VFX  
- Damage numbers  
- Status icons  
- Enemies (prefer pool when spawning waves)  
- One-shot audio emitter hosts  

Use Get → use → Release.

## Existing pools (keep using)

| Pool | Use for |
|------|---------|
| `AbilityVfxPool` | Ability particle/sprite VFX (`PooledAbilityVfx`) |
| `FloatingDamagePool` | Damage / mana numbers |
| `CombatFeedbackBurst` | Hit/death particle bursts |
| `CombatStatusEffectPool` | Stun/poison visuals |
| `PoolService` | **New** generic prefab Get/Release for M2 actives |

## PoolService

```csharp
GameObject fx = PoolService.Get(prefab, position, rotation);
// ...
PoolService.Release(fx);
```

Respects `MobileQualityRuntime.MaxConcurrentVfx` and particle budget scale.

## Mobile quality coupling

Pools/consumers read `MobileQualityRuntime` (updated once on quality change via `MobileQualityService.OnQualityChanged`):

- Screen shake off on Low  
- Damage numbers / status icons gated  
- VFX concurrent caps  
- Particle count scale  

## M2 actives

Meteor Strike, Frost Nova, etc. **must** spawn presentation through `AbilityVfxPool` or `PoolService`, never raw Instantiate in Update/cast hot path.

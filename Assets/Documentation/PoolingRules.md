# Pooling Rules (M2 / M3 prerequisite)

## Hard rule for combat (M2+)

**Do not** `Instantiate` / `Destroy` repeatedly during combat for:

- Ability VFX  
- Projectile / impact VFX  
- Damage numbers  
- Status icons  
- One-shot audio emitter hosts  

Use Get → use → Release.

## Existing pools (keep using)

| Pool | Use for |
|------|---------|
| `AbilityVfxPool` | Ability particle/sprite VFX (`PooledAbilityVfx`) |
| `FloatingDamagePool` | Damage / mana numbers |
| `CombatFeedbackBurst` | Hit/death particle bursts |
| `CombatStatusEffectPool` | Stun/poison visuals |
| `PoolService` | Generic prefab Get/Release for M2 actives (double-release safe via `PooledInstance.IsInPool`) |

## PoolService

```csharp
GameObject fx = PoolService.Get(prefab, position, rotation);
// ...
PoolService.Release(fx);
PoolService.Release(fx); // safe no-op
```

Respects `MobileQualityRuntime.MaxConcurrentVfx` and particle budget scale.

## Enemy pooling — deferred to Milestone 3

**Do not** migrate `WaveBossManager` / enemy spawn to `PoolService` in M1/M2.

Current Instantiate/Destroy for enemies may remain until **M3**, when these move together:

- `EnemyDefinition` prefab wiring  
- `WaveTable` consumption  
- `WaveBossManager` spawn path  
- Enemy object pooling  

Documented intentionally so M2 ability work does not invent a half-migration.

## Mobile quality coupling

Pools/consumers read `MobileQualityRuntime` (updated once on quality change via `MobileQualityService.OnQualityChanged`).

## M2 actives

Meteor Strike, Frost Nova, etc. **must** spawn presentation through `AbilityVfxPool` or `PoolService`, never raw Instantiate in Update/cast hot path.

# Mobile Optimization Checklist

## Quality tiers (runtime-enforced)

| Tier | FPS | VFX cap | Numbers cap | Shake | Status | Particles |
|------|-----|---------|--------------|-------|--------|-----------|
| Low | 30 | 12 | 16 | Off | Off | 0.5× |
| Mid | 60 | 24 | 48 | On | On | 0.85× |
| High | 60 | 40 | 64 | On | On | 1.0× |

`MobileQualityService` applies FPS + updates `MobileQualityRuntime` once, then fires `OnQualityChanged`.

Wired consumers:

- `CombatScreenShake` — respects EnableScreenShake  
- `FloatingDamagePool` — EnableDamageNumbers + concurrent cap  
- `EnemyCombatFeedback` / status pool — EnableStatusIcons  
- `AbilityVfxPool` / `PoolService` — MaxConcurrentVfx + particle scale  
- `CombatFeedbackBurst` — particle count scaled  

## Rules

1. Pool combat FX — see [PoolingRules.md](PoolingRules.md)  
2. Subscribe to `OnQualityChanged` for one-shot reconfigure — do not poll every frame  
3. Addressables for heavy content — see [AddressablesFoundation.md](AddressablesFoundation.md)  
4. Validate content: **Game → Foundation → Validate Game Content**  

## Editor verification

1. Play Bootstrap → `[MobileQuality] Applied ... shake=...`  
2. **Quality → Force Low** — shake/numbers/status reduce  
3. **Validate Game Content** — zero errors before M2 authoring  
4. **Initialize Addressables Groups** once after package import  

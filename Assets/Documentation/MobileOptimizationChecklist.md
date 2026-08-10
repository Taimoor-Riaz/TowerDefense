# Mobile Optimization Checklist (Day 4)

## Quality tiers

| Tier | FPS | VFX cap | Shake | Particles |
|------|-----|---------|-------|-----------|
| Low | 30 | 12 | Off | 0.5× |
| Mid | 60 | 24 | On | 0.85× |
| High | 60 | 40 | On | 1.0× |

Assets: `Assets/Content/Quality/MobileQuality_*.asset`  
Runtime catalog: `Assets/Resources/MobileQualityCatalog.asset`  
Service: `MobileQualityService` on Bootstrap `GameServices`

Auto-detect: RAM &lt; 3072 MB → Low; &lt; 6144 → Mid; else High.  
Override: `MobileQualityService.SetTierManual` or PlayerPrefs `MOBILE_QUALITY_TIER`.

## Rules for all future systems

1. **Pool** bullets, enemies, damage numbers, status icons, ability VFX (`AbilityVfxPool`, `FloatingDamagePool`, etc.). Do not `Instantiate`/`Destroy` per hit in hot paths.
2. **Respect** `MobileQualityService.Instance.ActiveProfile` for shake / VFX caps when adding juice.
3. **Canvases** — prefer separate static vs dynamic canvases; avoid full-screen rebuilds every frame.
4. **URP 2D** — keep post-processing light on Low (`reducePostProcessing`); avoid extra full-screen passes.
5. **Textures** — ASTC on Android; trim oversized UI atlases.
6. **Audio** — compress clips; limit simultaneous one-shots.
7. **Safe area** — hub already has safe-area hooks; keep notches in mind for new HUD.

## Editor verification

1. Play Bootstrap → console `[MobileQuality] Applied ...`
2. **Game → Foundation → Validate Mobile Quality**
3. Play Mode: **Game → Foundation → Quality → Force Low/Mid/High**

## Android smoke (device)

See [Android_BuildSmoke.md](Android_BuildSmoke.md).

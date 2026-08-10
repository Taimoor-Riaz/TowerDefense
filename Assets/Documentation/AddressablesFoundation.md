# Addressables Foundation (local)

Package: `com.unity.addressables` (added to `Packages/manifest.json`).

## Setup (once in Unity)

1. Open project so Package Manager resolves Addressables.  
2. Menu: **Game → Foundation → Initialize Addressables Groups**  
3. Groups created (local build/load paths): Core, Units, Abilities, Enemies, Bosses, VFX, Audio, UI  

## Runtime API

```csharp
var handle = AddressableContent.LoadAssetHandleAsync<GameObject>("VFX/MeteorImpact");
await handle.Task;
// use handle.Result
AddressableContent.Release(handle);

// Or instantiate:
GameObject go = await AddressableContent.InstantiateAsync("Enemies/Runner", pos, rot);
AddressableContent.ReleaseInstance(go);
```

**Rule:** Load → Use → **Release**. Never load-and-forget.

## Migration scope

Do **not** convert the whole project now. Prefer Addressables for heavy M2/M3 content:

- Ability VFX / audio  
- Enemy / boss prefabs  
- Large UI art  

Config ScriptableObjects stay in `Assets/Content` + `GameConfigRegistry`.

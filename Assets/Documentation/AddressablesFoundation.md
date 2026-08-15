# Addressables Foundation (local)

Package: `com.unity.addressables` (in `Packages/manifest.json`).

## Setup (required once)

1. Open the project in Unity (Package Manager resolves Addressables).  
2. Menu: **Game → Foundation → Initialize Addressables Groups**  
   - Batch: `Unity -batchmode -quit -executeMethod AddressablesFoundationMenu.InitializeAddressablesGroupsBatch`  
3. Commit generated `Assets/AddressableAssetsData/` (settings + groups).  
4. Groups (local build/load paths): Core, Units, Abilities, Enemies, Bosses, VFX, Audio, UI  

If `AddressableAssetsData` already exists (it does), skip Initialize. Groups stay empty until M2/M3 heavy content.

## Runtime API

```csharp
var handle = AddressableContent.LoadAssetHandleAsync<GameObject>("VFX/MeteorImpact");
await handle.Task;
// use handle.Result
AddressableContent.Release(handle);

GameObject go = await AddressableContent.InstantiateAsync("Enemies/Runner", pos, rot);
AddressableContent.ReleaseInstance(go);
```

**Rule:** Load → Use → **Release**. Never load-and-forget.

## Migration scope

Do **not** convert the whole project now. Prefer Addressables for heavy M2/M3 content:

- Ability VFX / audio  
- Enemy / boss prefabs  
- Large UI art  

Config ScriptableObjects stay on `GameConfigRegistry` (`Assets/Content/Resources/GameConfigRegistry.asset`).

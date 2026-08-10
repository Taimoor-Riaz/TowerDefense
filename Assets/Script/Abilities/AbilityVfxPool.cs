using System.Collections.Generic;
using UnityEngine;

public sealed class AbilityVfxPool : MonoBehaviour
{
    private readonly Dictionary<int, Queue<PooledAbilityVfx>> available = new Dictionary<int, Queue<PooledAbilityVfx>>();
    private Transform inactiveRoot;

    public static AbilityVfxPool Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Instance = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance == null)
            new GameObject("Ability VFX Pool", typeof(AbilityVfxPool));
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        inactiveRoot = new GameObject("Inactive Ability VFX").transform;
        inactiveRoot.SetParent(transform, false);
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : PooledAbilityVfx
    {
        return Instance != null ? Instance.Take(prefab, position, rotation) : null;
    }

    private T Take<T>(T prefab, Vector3 position, Quaternion rotation) where T : PooledAbilityVfx
    {
        if (prefab == null)
            return null;

        int key = prefab.GetInstanceID();
        if (!available.TryGetValue(key, out Queue<PooledAbilityVfx> queue))
        {
            queue = new Queue<PooledAbilityVfx>();
            available.Add(key, queue);
        }

        PooledAbilityVfx item = null;
        while (queue.Count > 0 && item == null)
            item = queue.Dequeue();

        if (item == null)
        {
            item = Instantiate(prefab, inactiveRoot);
            item.name = prefab.name;
            item.AssignPool(this, key);
        }

        Transform itemTransform = item.transform;
        itemTransform.SetParent(null, false);
        itemTransform.SetPositionAndRotation(position, rotation);
        item.gameObject.SetActive(true);
        item.OnTakenFromPool();
        return item as T;
    }

    internal void Return(PooledAbilityVfx item, int key)
    {
        if (item == null)
            return;

        item.OnReturnedToPool();
        item.gameObject.SetActive(false);
        item.transform.SetParent(inactiveRoot, false);

        if (!available.TryGetValue(key, out Queue<PooledAbilityVfx> queue))
        {
            queue = new Queue<PooledAbilityVfx>();
            available.Add(key, queue);
        }

        queue.Enqueue(item);
    }
}

public abstract class PooledAbilityVfx : MonoBehaviour
{
    private AbilityVfxPool ownerPool;
    private int poolKey;
    private bool isSpawned;

    protected bool IsSpawned => isSpawned;

    internal void AssignPool(AbilityVfxPool pool, int key)
    {
        ownerPool = pool;
        poolKey = key;
    }

    internal virtual void OnTakenFromPool()
    {
        isSpawned = true;
    }

    internal virtual void OnReturnedToPool()
    {
        isSpawned = false;
    }

    protected void Release()
    {
        if (!isSpawned)
            return;

        isSpawned = false;
        if (ownerPool != null)
            ownerPool.Return(this, poolKey);
        else
            gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public static ManaManager Instance { get; private set; }
    public static event Action<int> OnManaChanged;
    public static event Action<int> OnSummonCostChanged;

    [Header("Mana Settings")]
    [SerializeField, Min(0)] private int startingMana = 130;
    [SerializeField] private bool useInspectorStartingMana = true;
    [SerializeField] private bool allowRuntimeInspectorManaChanges = true;
    [SerializeField, Min(0)] private int currentMana = 130;

    [Header("Summon Cost Settings")]
    [SerializeField, Min(0)] private int initialSummonCost = 50;
    [SerializeField, Min(0)] private int summonCostIncrease = 10;

    public int CurrentMana => currentMana;
    public int CurrentSummonCost { get; private set; }

    private int lastInspectorMana;
    private bool initialized;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // Only initialize in battle scene if needed, or always initialize
        // But ManaManager should probably be in the scene since it has serialized fields
    }

    private void Awake()
    {
        Instance = this;

        if (useInspectorStartingMana || CurrencyManager.Instance == null)
        {
            SetManaInternal(startingMana, false);
        }
        else
        {
            SetManaInternal(CurrencyManager.Instance.Water, false);
        }

        CurrentSummonCost = initialSummonCost;

        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.SetSummonCost(CurrentSummonCost);

        lastInspectorMana = currentMana;
        initialized = true;
    }

    private void Start()
    {
        OnManaChanged?.Invoke(currentMana);
        OnSummonCostChanged?.Invoke(CurrentSummonCost);
    }

    private void Update()
    {
        if (!allowRuntimeInspectorManaChanges || !initialized)
            return;

        if (currentMana == lastInspectorMana)
            return;

        SetMana(currentMana);
    }

    private void OnValidate()
    {
        startingMana = Mathf.Max(0, startingMana);
        currentMana = Mathf.Max(0, currentMana);
        initialSummonCost = Mathf.Max(0, initialSummonCost);
        summonCostIncrease = Mathf.Max(0, summonCostIncrease);

        if (!Application.isPlaying && useInspectorStartingMana)
            currentMana = startingMana;
    }

    public bool SpendMana(int amount)
    {
        if (!BattleFlowState.IsGameplayActive || amount < 0)
            return false;

        if (currentMana < amount)
            return false;

        SetMana(currentMana - amount);
        return true;
    }

    public void AddMana(int amount)
    {
        SetMana(currentMana + amount);
    }

    public int AddManaCapped(int amount, int maximumMana)
    {
        int before = currentMana;
        int cap = maximumMana > 0 ? Mathf.Max(before, maximumMana) : int.MaxValue;
        SetMana(Mathf.Min(cap, currentMana + Mathf.Max(0, amount)));
        return currentMana - before;
    }

    public void SetMana(int amount)
    {
        SetManaInternal(amount, true);
    }

    private void SetManaInternal(int amount, bool notify)
    {
        currentMana = Mathf.Max(0, amount);
        lastInspectorMana = currentMana;
        
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.SetWater(currentMana);

        if (notify)
            OnManaChanged?.Invoke(currentMana);
    }

    public void IncreaseSummonCost()
    {
        CurrentSummonCost += summonCostIncrease;

        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.SetSummonCost(CurrentSummonCost);

        OnSummonCostChanged?.Invoke(CurrentSummonCost);
    }
}

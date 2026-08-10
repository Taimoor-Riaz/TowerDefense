using UnityEngine;

/// <summary>
/// Single source of truth for game configuration assets.
/// Bootstrap / GameServices holds one registry; do not duplicate configs under Resources.
/// </summary>
[CreateAssetMenu(fileName = "GameConfigRegistry", menuName = "Game/Core/Game Config Registry")]
public class GameConfigRegistry : ScriptableObject
{
    [Header("Core")]
    [SerializeField] private SceneFlowConfig sceneFlow;
    [SerializeField] private GameBalanceConfig gameBalance;
    [SerializeField] private MobileQualityCatalog mobileQuality;

    [Header("Content catalogs")]
    [SerializeField] private ActiveAbilityCatalog activeAbilities;
    [SerializeField] private WaveTable defaultWaveTable;

    public SceneFlowConfig SceneFlow => sceneFlow;
    public GameBalanceConfig GameBalance => gameBalance;
    public MobileQualityCatalog MobileQuality => mobileQuality;
    public ActiveAbilityCatalog ActiveAbilities => activeAbilities;
    public WaveTable DefaultWaveTable => defaultWaveTable;

    public static GameConfigRegistry LoadDefault()
    {
        return Resources.Load<GameConfigRegistry>("GameConfigRegistry");
    }
}

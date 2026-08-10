using UnityEngine;

/// <summary>
/// Applies mobile quality profiles (FPS, VFX budget flags). Lives on Bootstrap / GameServices.
/// Other systems can read <see cref="ActiveProfile"/> without hardcoding device checks.
/// </summary>
public class MobileQualityService : MonoBehaviour
{
    public static MobileQualityService Instance { get; private set; }

    private const string CatalogResourceName = "MobileQualityCatalog";
    private const string PrefsTierKey = "MOBILE_QUALITY_TIER";

    [SerializeField] private MobileQualityCatalog catalog;
    [SerializeField] private bool useAutoDetect = true;
    [SerializeField] private bool allowPlayerPrefsOverride = true;
    [SerializeField] private MobileQualityTier editorForcedTier = MobileQualityTier.Mid;

    public MobileQualityCatalog Catalog => catalog;
    public MobileQualityProfile ActiveProfile { get; private set; }
    public MobileQualityTier ActiveTier { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        ResolveCatalog();
        ApplySelectedTier();
    }

    private void ResolveCatalog()
    {
        if (catalog != null)
            return;

        catalog = Resources.Load<MobileQualityCatalog>(CatalogResourceName);
    }

    public void ApplySelectedTier()
    {
        MobileQualityTier tier = ResolveTier();
        ApplyTier(tier);
    }

    public void ApplyTier(MobileQualityTier tier)
    {
        ResolveCatalog();
        ActiveTier = tier;
        ActiveProfile = catalog != null ? catalog.GetProfile(tier) : null;

        if (ActiveProfile == null)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            Debug.LogWarning("[MobileQuality] No profile found — defaulted to 60 FPS.");
            return;
        }

        Application.targetFrameRate = Mathf.Clamp(ActiveProfile.targetFrameRate, 15, 120);
        QualitySettings.vSyncCount = ActiveProfile.vsync ? 1 : 0;

        Debug.Log(
            "[MobileQuality] Applied " + tier +
            " | FPS=" + Application.targetFrameRate +
            " | VFX cap=" + ActiveProfile.maxConcurrentVfx +
            " | particles=" + ActiveProfile.particleBudgetScale +
            " | RAM=" + SystemInfo.systemMemorySize + "MB");
    }

    public void SetTierManual(MobileQualityTier tier, bool persist = true)
    {
        if (persist && allowPlayerPrefsOverride)
        {
            PlayerPrefs.SetInt(PrefsTierKey, (int)tier);
            PlayerPrefs.Save();
        }

        ApplyTier(tier);
    }

    private MobileQualityTier ResolveTier()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return editorForcedTier;
#endif
        if (allowPlayerPrefsOverride && PlayerPrefs.HasKey(PrefsTierKey))
            return (MobileQualityTier)PlayerPrefs.GetInt(PrefsTierKey, (int)MobileQualityTier.Mid);

        if (useAutoDetect && catalog != null)
            return catalog.DetectTierFromDevice();

        if (catalog != null)
            return catalog.defaultTier;

        return editorForcedTier;
    }

    /// <summary>
    /// Ensures a quality service exists (editor play from non-Bootstrap scenes).
    /// </summary>
    public static MobileQualityService EnsureExists()
    {
        if (Instance != null)
            return Instance;

        GameServices services = GameServices.EnsureExists();
        var quality = services.GetComponent<MobileQualityService>();
        if (quality == null)
            quality = services.gameObject.AddComponent<MobileQualityService>();
        return quality;
    }
}

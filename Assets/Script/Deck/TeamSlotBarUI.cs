using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSlotBarUI : MonoBehaviour
{
    [Header("Optional References")]
    [SerializeField] private UnitData[] selectedDeckUnits;
    [SerializeField] private TowerBoardCell[] boardCells;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private TMP_Text[] levelTexts;

    [Header("Slot Style")]
    [SerializeField] private Color emptySlotColor = new Color(1f, 1f, 1f, 0.22f);
    [SerializeField] private Color occupiedSlotColor = Color.white;
    [SerializeField] private Color levelBadgeColor = new Color(0.02f, 0.025f, 0.025f, 0.82f);

    private readonly List<Image> resolvedSlotImages = new List<Image>();
    private readonly List<TMP_Text> resolvedLevelTexts = new List<TMP_Text>();

    private void OnEnable()
    {
        TowerBoardCell.BoardChanged += Refresh;
        SummonManager.SelectedDeckChanged += SetSelectedDeck;

        ResolveReferences();
        Refresh();
    }

    private void Start()
    {
        ResolveReferences();
        Refresh();
    }

    private void OnDisable()
    {
        TowerBoardCell.BoardChanged -= Refresh;
        SummonManager.SelectedDeckChanged -= SetSelectedDeck;
    }

    public void SetSelectedDeck(UnitData[] deckUnits)
    {
        selectedDeckUnits = deckUnits;
        Refresh();
    }

    public void Refresh()
    {
        ResolveReferences();

        for (int i = 0; i < resolvedSlotImages.Count; i++)
        {
            Image slotImage = resolvedSlotImages[i];
            TMP_Text levelText = i < resolvedLevelTexts.Count ? resolvedLevelTexts[i] : null;
            UnitData unitData = selectedDeckUnits != null && i < selectedDeckUnits.Length ? selectedDeckUnits[i] : null;

            if (slotImage == null)
                continue;

            if (unitData == null)
            {
                slotImage.sprite = null;
                slotImage.color = emptySlotColor;
                SetLevelText(levelText, string.Empty, false);
                continue;
            }

            int highestLevel = GetHighestBoardLevel(unitData);
            slotImage.sprite = unitData.GetIcon(highestLevel);
            slotImage.color = occupiedSlotColor;
            slotImage.preserveAspect = true;
            slotImage.raycastTarget = false;

            SetLevelText(levelText, "Lv." + highestLevel, true);
        }
    }

    private int GetHighestBoardLevel(UnitData unitData)
    {
        int highestLevel = 1;

        if (unitData == null || boardCells == null)
            return highestLevel;

        foreach (TowerBoardCell cell in boardCells)
        {
            BoardTower tower = cell != null ? cell.CurrentTower : null;

            if (tower == null || tower.UnitData != unitData)
                continue;

            highestLevel = Mathf.Max(highestLevel, Mathf.Max(1, tower.Level));
        }

        return highestLevel;
    }

    private void ResolveReferences()
    {
        ResolveDeck();
        ResolveBoardCells();
        ResolveSlots();
    }

    private void ResolveDeck()
    {
        if (SummonManager.Instance == null)
            return;

        UnitData[] managerDeck = SummonManager.Instance.SelectedDeckUnits;

        if (managerDeck != null && managerDeck.Length > 0)
            selectedDeckUnits = managerDeck;
    }

    private void ResolveBoardCells()
    {
        if (boardCells != null && boardCells.Length > 0)
            return;

        if (SummonManager.Instance != null && SummonManager.Instance.BoardCells != null && SummonManager.Instance.BoardCells.Length > 0)
        {
            boardCells = SummonManager.Instance.BoardCells;
            return;
        }

        boardCells = FindObjectsByType<TowerBoardCell>(FindObjectsSortMode.None);
    }

    private void ResolveSlots()
    {
        resolvedSlotImages.Clear();
        resolvedLevelTexts.Clear();

        if (slotImages != null && slotImages.Length > 0)
        {
            foreach (Image slotImage in slotImages)
            {
                if (slotImage != null)
                    resolvedSlotImages.Add(slotImage);
            }
        }
        else
        {
            Image[] childImages = GetComponentsInChildren<Image>(true);

            foreach (Image image in childImages)
            {
                if (image == null || image.transform == transform)
                    continue;

                if (image.gameObject.name.Contains("TeamSlot", StringComparison.OrdinalIgnoreCase))
                    resolvedSlotImages.Add(image);
            }

            resolvedSlotImages.Sort((left, right) => GetHierarchyOrder(left.transform).CompareTo(GetHierarchyOrder(right.transform)));
        }

        if (levelTexts != null && levelTexts.Length > 0)
        {
            foreach (TMP_Text text in levelTexts)
            {
                if (text != null)
                    resolvedLevelTexts.Add(text);
            }
        }

        while (resolvedLevelTexts.Count < resolvedSlotImages.Count)
            resolvedLevelTexts.Add(EnsureLevelText(resolvedSlotImages[resolvedLevelTexts.Count]));
    }

    private TMP_Text EnsureLevelText(Image slotImage)
    {
        if (slotImage == null)
            return null;

        Transform existing = slotImage.transform.Find("LevelBadge/LevelText");

        if (existing != null && existing.TryGetComponent(out TMP_Text existingText))
            return existingText;

        RectTransform badge = new GameObject("LevelBadge", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)).GetComponent<RectTransform>();
        badge.gameObject.layer = slotImage.gameObject.layer;
        badge.SetParent(slotImage.transform, false);
        badge.anchorMin = new Vector2(0f, 0f);
        badge.anchorMax = new Vector2(1f, 0f);
        badge.pivot = new Vector2(0.5f, 0f);
        badge.anchoredPosition = new Vector2(0f, 0f);
        badge.sizeDelta = new Vector2(-8f, 24f);

        Image badgeImage = badge.GetComponent<Image>();
        badgeImage.color = levelBadgeColor;
        badgeImage.raycastTarget = false;

        RectTransform textTransform = new GameObject("LevelText", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI), typeof(Shadow)).GetComponent<RectTransform>();
        textTransform.gameObject.layer = slotImage.gameObject.layer;
        textTransform.SetParent(badge, false);
        textTransform.anchorMin = Vector2.zero;
        textTransform.anchorMax = Vector2.one;
        textTransform.offsetMin = new Vector2(2f, 0f);
        textTransform.offsetMax = new Vector2(-2f, 0f);

        TextMeshProUGUI text = textTransform.GetComponent<TextMeshProUGUI>();
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.enableAutoSizing = true;
        text.fontSizeMin = 10f;
        text.fontSizeMax = 18f;
        text.fontStyle = FontStyles.Bold;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.raycastTarget = false;

        Shadow shadow = textTransform.GetComponent<Shadow>();
        shadow.effectColor = new Color(0f, 0f, 0f, 0.85f);
        shadow.effectDistance = new Vector2(1.4f, -1.4f);

        return text;
    }

    private static void SetLevelText(TMP_Text text, string value, bool visible)
    {
        if (text == null)
            return;

        text.text = value;

        if (text.transform.parent != null)
            text.transform.parent.gameObject.SetActive(visible);
    }

    private static int GetHierarchyOrder(Transform transform)
    {
        if (transform == null)
            return int.MaxValue;

        int order = transform.GetSiblingIndex();
        Transform current = transform.parent;
        int multiplier = 100;

        while (current != null)
        {
            order += current.GetSiblingIndex() * multiplier;
            multiplier *= 100;
            current = current.parent;
        }

        return order;
    }
}

using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public static class CanvasMapSpace
{
    private const string MapCanvasName = "Canvas_Map";
    private const string MapRectName = "Map_Bg";
    private const string GameplaySortingLayerName = "Tower";
    private const float GameplayPlaneZ = 0f;
    private const int MapSortingOrder = -100;
    private const int HudSortingOrder = 500;

    private static readonly float[] GridXNormalized =
    {
        0.2530f,
        0.3748f,
        0.4968f,
        0.6179f,
        0.7390f
    };

    private static readonly float[] GridYNormalizedFromTop =
    {
        0.2261f,
        0.2872f,
        0.3490f,
        0.4116f,
        0.4743f
    };

    private static readonly Vector2[] LeftRouteWaypoints =
    {
        new Vector2(0.4861f, 0.6205f),
        new Vector2(0.4769f, 0.5809f),
        new Vector2(0.4343f, 0.5580f),
        new Vector2(0.3509f, 0.5471f),
        new Vector2(0.2287f, 0.5476f),
        new Vector2(0.1861f, 0.5429f),
        new Vector2(0.1324f, 0.4523f),
        new Vector2(0.1278f, 0.3643f),
        new Vector2(0.1278f, 0.2591f),
        new Vector2(0.1407f, 0.1955f),
        new Vector2(0.1824f, 0.1533f),
        new Vector2(0.2907f, 0.1585f),
        new Vector2(0.4065f, 0.1622f),
        new Vector2(0.4870f, 0.1289f)
    };

    private static readonly Vector2[] RightRouteWaypoints =
    {
        new Vector2(0.4907f, 0.6057f),
        new Vector2(0.4907f, 0.5703f),
        new Vector2(0.5917f, 0.5490f),
        new Vector2(0.7306f, 0.5474f),
        new Vector2(0.8491f, 0.5302f),
        new Vector2(0.8620f, 0.4479f),
        new Vector2(0.8602f, 0.3729f),
        new Vector2(0.8556f, 0.3042f),
        new Vector2(0.8583f, 0.2432f),
        new Vector2(0.8398f, 0.1859f),
        new Vector2(0.7667f, 0.1516f),
        new Vector2(0.6917f, 0.1547f),
        new Vector2(0.5250f, 0.1500f),
        new Vector2(0.5056f, 0.1349f)
    };

    private static int lastCanvasForceUpdateFrame = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void ConfigureLoadedBattleScene()
    {
        ConfigureMapCanvasForGameplay();
    }

    public static void ConfigureMapCanvasForGameplay()
    {
        Canvas mapCanvas = FindMapCanvas();

        if (mapCanvas == null)
            return;

        Camera gameplayCamera = Camera.main;

        mapCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        mapCanvas.worldCamera = gameplayCamera;
        mapCanvas.overrideSorting = true;
        mapCanvas.sortingLayerName = "Default";
        mapCanvas.sortingOrder = MapSortingOrder;

        if (gameplayCamera != null)
        {
            float gameplayDepth = Mathf.Abs(gameplayCamera.transform.position.z - GameplayPlaneZ);
            mapCanvas.planeDistance = Mathf.Clamp(
                gameplayDepth + 1f,
                gameplayCamera.nearClipPlane + 0.01f,
                gameplayCamera.farClipPlane - 0.01f
            );
        }

        RectTransform mapRect = FindMapRect();
        if (mapRect != null && mapRect.TryGetComponent(out Image mapImage))
            mapImage.raycastTarget = false;

        ConfigureForegroundUi(mapCanvas, mapRect);
        ForceCanvasUpdateOncePerFrame();
    }

    public static bool TryGetBoardCellWorldPosition(string cellName, out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;

        if (!TryGetCellIndex(cellName, out int cellIndex))
            return false;

        int zeroBasedIndex = cellIndex - 1;
        int row = zeroBasedIndex / 5;
        int column = zeroBasedIndex % 5;

        if (row < 0 || row >= GridYNormalizedFromTop.Length ||
            column < 0 || column >= GridXNormalized.Length)
        {
            return false;
        }

        return TryGetMapNormalizedWorldPosition(
            GridXNormalized[column],
            GridYNormalizedFromTop[row],
            out worldPosition
        );
    }

    public static bool TryGetRouteWaypointWorldPosition(string routeName, int waypointIndex, out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;

        Vector2[] routeWaypoints = GetRouteWaypoints(routeName);

        if (routeWaypoints == null || waypointIndex < 0 || waypointIndex >= routeWaypoints.Length)
            return false;

        Vector2 normalizedPosition = routeWaypoints[waypointIndex];
        return TryGetMapNormalizedWorldPosition(normalizedPosition.x, normalizedPosition.y, out worldPosition);
    }

    public static Vector3 TransformToGameplayWorld(Transform source)
    {
        if (source == null)
            return Vector3.zero;

        Canvas sourceCanvas = source.GetComponentInParent<Canvas>();

        if (sourceCanvas == null)
            return source.position;

        Camera canvasCamera = GetCanvasCamera(sourceCanvas);

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(canvasCamera, source.position);

        return ScreenToGameplayWorld(screenPosition);
    }

    private static bool TryGetMapNormalizedWorldPosition(float normalizedX, float normalizedYFromTop, out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;

        ConfigureMapCanvasForGameplay();

        RectTransform mapRect = FindMapRect();
        Canvas mapCanvas = FindMapCanvas();

        if (mapRect == null || mapCanvas == null)
            return false;

        ForceCanvasUpdateOncePerFrame();

        Vector3[] corners = new Vector3[4];
        mapRect.GetWorldCorners(corners);

        Camera canvasCamera = GetCanvasCamera(mapCanvas);

        Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(canvasCamera, corners[0]);
        Vector2 topRight = RectTransformUtility.WorldToScreenPoint(canvasCamera, corners[2]);

        Vector2 screenPosition = new Vector2(
            Mathf.Lerp(bottomLeft.x, topRight.x, normalizedX),
            Mathf.Lerp(topRight.y, bottomLeft.y, normalizedYFromTop)
        );

        worldPosition = ScreenToGameplayWorld(screenPosition);
        return true;
    }

    private static void ConfigureForegroundUi(Canvas mapCanvas, RectTransform mapRect)
    {
        if (mapCanvas == null)
            return;

        Transform canvasTransform = mapCanvas.transform;

        for (int i = 0; i < canvasTransform.childCount; i++)
        {
            Transform child = canvasTransform.GetChild(i);

            if (child == null || child == mapRect || child.GetComponent<RectTransform>() == null)
                continue;

            ConfigureForegroundCanvas(child.gameObject, HudSortingOrder);
        }

        if (mapRect == null)
            return;

        ConfigureForegroundCanvas(mapRect.Find("TowerInfoPopup")?.gameObject, HudSortingOrder + 10);
        ConfigureForegroundCanvas(mapRect.Find("WavePopup")?.gameObject, HudSortingOrder + 20);
    }

    private static void ConfigureForegroundCanvas(GameObject target, int sortingOrder)
    {
        if (target == null)
            return;

        Canvas canvas = target.GetComponent<Canvas>();
        if (canvas == null)
            canvas = target.AddComponent<Canvas>();

        canvas.overrideSorting = true;
        canvas.sortingLayerName = GameplaySortingLayerName;
        canvas.sortingOrder = sortingOrder;

        if (target.GetComponent<GraphicRaycaster>() == null)
            target.AddComponent<GraphicRaycaster>();
    }

    private static Vector2[] GetRouteWaypoints(string routeName)
    {
        if (string.IsNullOrWhiteSpace(routeName))
            return null;

        if (routeName.IndexOf("left", StringComparison.OrdinalIgnoreCase) >= 0)
            return LeftRouteWaypoints;

        if (routeName.IndexOf("right", StringComparison.OrdinalIgnoreCase) >= 0)
            return RightRouteWaypoints;

        return null;
    }

    public static Vector3 ScreenToGameplayWorld(Vector2 screenPosition)
    {
        Camera gameplayCamera = Camera.main;

        if (gameplayCamera == null)
            return new Vector3(screenPosition.x, screenPosition.y, GameplayPlaneZ);

        float depth = Mathf.Abs(gameplayCamera.transform.position.z - GameplayPlaneZ);
        Vector3 worldPosition = gameplayCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));
        worldPosition.z = GameplayPlaneZ;

        return worldPosition;
    }

    private static bool TryGetCellIndex(string cellName, out int index)
    {
        index = 0;

        if (string.IsNullOrWhiteSpace(cellName))
            return false;

        int underscoreIndex = cellName.LastIndexOf('_');

        if (underscoreIndex < 0 || underscoreIndex >= cellName.Length - 1)
            return false;

        string numberText = cellName.Substring(underscoreIndex + 1);
        return int.TryParse(numberText, NumberStyles.Integer, CultureInfo.InvariantCulture, out index);
    }

    private static Canvas FindMapCanvas()
    {
        GameObject canvasObject = GameObject.Find(MapCanvasName);
        return canvasObject != null ? canvasObject.GetComponent<Canvas>() : null;
    }

    private static RectTransform FindMapRect()
    {
        GameObject mapObject = GameObject.Find(MapRectName);
        return mapObject != null ? mapObject.GetComponent<RectTransform>() : null;
    }

    private static Camera GetCanvasCamera(Canvas canvas)
    {
        if (canvas == null || canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            return null;

        return canvas.worldCamera != null ? canvas.worldCamera : Camera.main;
    }

    private static void ForceCanvasUpdateOncePerFrame()
    {
        if (lastCanvasForceUpdateFrame == Time.frameCount)
            return;

        Canvas.ForceUpdateCanvases();
        lastCanvasForceUpdateFrame = Time.frameCount;
    }
}

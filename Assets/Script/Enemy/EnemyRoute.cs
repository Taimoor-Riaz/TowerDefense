using UnityEngine;

public class EnemyRoute : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    [Header("Route Rotation")]
    [SerializeField] private float routeRotationOffset = 0f;

    public Transform[] Waypoints => waypoints;
    public int WaypointCount => waypoints != null ? waypoints.Length : 0;
    public float RouteRotationOffset => routeRotationOffset;

    private void Awake()
    {
        RefreshWaypoints();
    }

    public Vector3 GetWaypointPosition(int index)
    {
        if (waypoints == null || index < 0 || index >= waypoints.Length || waypoints[index] == null)
            return transform.position;

        if (CanvasMapSpace.TryGetRouteWaypointWorldPosition(gameObject.name, index, out Vector3 normalizedPosition))
            return normalizedPosition;

        return CanvasMapSpace.TransformToGameplayWorld(waypoints[index]);
    }

    private void OnValidate()
    {
        RefreshWaypoints();
    }

    private void RefreshWaypoints()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            waypoints[i] = transform.GetChild(i);
    }
}

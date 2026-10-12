using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private Vector3[] waypoints = new Vector3[4];
    private int currentWaypoint = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 pos = transform.TransformPoint(waypoints[i]);
            Gizmos.DrawSphere(pos, 0.3f);

            if (i < waypoints.Length - 1)
                Gizmos.DrawLine(pos, transform.TransformPoint(waypoints[i + 1]));
            else
                Gizmos.DrawLine(pos, transform.TransformPoint(waypoints[0]));
        }
    }

    public Vector3 GetCurrentWaypoint()
    {
        if (waypoints.Length == 0) return transform.position;
        return transform.TransformPoint(waypoints[currentWaypoint]);
    }

    public Vector3 GetNextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        return GetCurrentWaypoint();
    }

    public float GetWaypointDistance()
    {
        return 0.5f;
    }
}

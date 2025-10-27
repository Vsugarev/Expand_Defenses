using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextWaypoints = new List<Waypoint>();
    public bool isEndPoint = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);

        Gizmos.color = Color.red;
        foreach (var next in nextWaypoints)
        {
            if (next != null)
                Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}

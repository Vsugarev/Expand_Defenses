using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Los waypoints a los que se puede ir desde aquí")]
    public List<Waypoint> nextWaypoints = new List<Waypoint>();

    // Esto dibuja los puntos y líneas en la vista de escena
    private void OnDrawGizmos()
    {
        // Dibuja un cubo para visualizar el waypoint
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);

        // Dibuja líneas rojas hacia los siguientes waypoints
        Gizmos.color = Color.red;
        foreach (var next in nextWaypoints)
        {
            if (next != null)
            {
                Gizmos.DrawLine(transform.position, next.transform.position);
            }
        }
    }
}
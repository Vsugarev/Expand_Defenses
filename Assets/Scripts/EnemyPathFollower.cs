using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public Waypoint currentWaypoint;
    public float speed = 2f;
    public float maxOffset = 0.3f; // máxima desviación

    private Vector3 targetPosition;
    private Vector2 pathOffset;

    void Start()
    {
        // Genera un offset único para este enemigo
        pathOffset = Random.insideUnitCircle * maxOffset;

        SetTargetPosition();
        transform.position = targetPosition;
    }

    void Update()
    {
        if (currentWaypoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 dir = targetPosition - transform.position;
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (currentWaypoint.nextWaypoints.Count > 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoints[Random.Range(0, currentWaypoint.nextWaypoints.Count)];
                SetTargetPosition();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void SetTargetPosition()
    {
        Vector3 basePos = currentWaypoint.transform.position;
        targetPosition = new Vector3(basePos.x + pathOffset.x, basePos.y + pathOffset.y, basePos.z);
    }
}


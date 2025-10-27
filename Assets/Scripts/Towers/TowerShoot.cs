using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootRate = 1f;
    public float range = 5f;

    private float shootTimer;

    void Update()
    {
        shootTimer -= Time.deltaTime;

        GameObject enemy = FindNearestEnemy();
        if (enemy != null && shootTimer <= 0f)
        {
            Shoot(enemy.transform.position);
            shootTimer = shootRate;
        }
    }

    GameObject FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        float minDist = Mathf.Infinity;
        GameObject nearest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = hit.gameObject;
                }
            }
        }
        return nearest;
    }

    void Shoot(Vector3 target)
    {
        if (projectilePrefab != null)
        {
            Vector3 dir = (target - shootPoint.position).normalized;
            GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = dir * 5f;
        }
    }
}

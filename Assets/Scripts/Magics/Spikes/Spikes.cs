using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float slowAmount = 0.5f;
    public float duration = 1f;

    private List<EnemyPathFollower> affectedEnemies = new List<EnemyPathFollower>();

    void Start()
    {
        ApplySlow();
        Destroy(gameObject, duration);
    }

    void ApplySlow()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D hit in hits)
        {
            EnemyPathFollower enemy = hit.GetComponent<EnemyPathFollower>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                enemy.speed *= slowAmount;
                enemy.chaseSpeed *= slowAmount;
                affectedEnemies.Add(enemy);
            }
        }
    }

    void OnDestroy()
    {
        RemoveSlow();
    }

    void RemoveSlow()
    {
        foreach (var enemy in affectedEnemies)
        {
            if (enemy != null)
            {
                enemy.speed /= slowAmount;
                enemy.chaseSpeed /= slowAmount;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}

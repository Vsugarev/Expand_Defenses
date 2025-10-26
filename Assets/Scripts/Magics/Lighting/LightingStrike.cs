using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float damageRadius = 2f;
    public int damageAmount = 1;
    public float duration = 0.5f;
    public LayerMask enemyLayer;

    private HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

    void Start()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, damageRadius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyHealth health = hit.GetComponent<EnemyHealth>();
            if (health != null && !damagedEnemies.Contains(health))
            {
                health.ChangeHealth(-damageAmount);
                damagedEnemies.Add(health);
            }
        }

        Destroy(gameObject, duration);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

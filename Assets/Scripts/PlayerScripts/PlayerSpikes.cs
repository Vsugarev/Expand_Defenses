using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaSpikes : MonoBehaviour
{
    public float duration = 2f;
    public float slowAmount = 0.5f;
    public float radius = 2f;

    private List<EnemyPathFollower> affectedEnemies = new List<EnemyPathFollower>();

    void Start()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D col in hitEnemies)
        {
            EnemyPathFollower epf = col.GetComponent<EnemyPathFollower>();
            if (epf != null)
            {
                epf.speed *= slowAmount;
                affectedEnemies.Add(epf);
            }
        }

        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(duration);

        foreach (EnemyPathFollower epf in affectedEnemies)
        {
            if (epf != null)
                epf.speed /= slowAmount;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

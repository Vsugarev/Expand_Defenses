using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2f;
    public float speed = 5f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public Transform target; // enemigo objetivo

    void Start()
    {
        if(target != null)
            direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn);
    }

    void Update()
    {
        if(target != null)
        {
            direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
            RotateArrow();
        }
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if(enemy != null)
                enemy.ChangeHealth(-damage);

            Destroy(gameObject);
        }
    }

    // Método para asignar el objetivo (opcional)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

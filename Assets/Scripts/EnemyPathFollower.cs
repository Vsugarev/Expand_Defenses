using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public Waypoint currentWaypoint;
    public float speed = 2f;
    public float chaseSpeed = 3f;
    public float stopDistance = 0.1f;

    private Vector3 targetPosition;
    private Transform player;
    private bool isChasing = false;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (currentWaypoint == null)
            currentWaypoint = FindClosestWaypoint();

        transform.position = transform.position;
        ChooseNextWaypoint();
    }

    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        if (isChasing && player != null)
        {
            moveDir = (player.position - transform.position);
            if (moveDir.magnitude > stopDistance)
                moveDir.Normalize();
            rb.velocity = moveDir * chaseSpeed;
        }
        else
        {
            moveDir = (targetPosition - transform.position);
            if (moveDir.magnitude > stopDistance)
                moveDir.Normalize();
            rb.velocity = moveDir * speed;

            if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
            {
                if (currentWaypoint.isEndPoint)
                    ReachEndPoint();
                else
                    ChooseNextWaypoint();
            }
        }

        UpdateAnimator(moveDir);
    }

    public void ChooseNextWaypoint()
    {
        if (currentWaypoint != null && currentWaypoint.nextWaypoints.Count > 0)
        {
            currentWaypoint = currentWaypoint.nextWaypoints[Random.Range(0, currentWaypoint.nextWaypoints.Count)];
            targetPosition = currentWaypoint.transform.position;
        }
        else if(currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;
        }
    }

    public Waypoint FindClosestWaypoint()
    {
        Waypoint[] allWaypoints = FindObjectsOfType<Waypoint>();
        Waypoint closest = null;
        float minDist = Mathf.Infinity;

        foreach (Waypoint wp in allWaypoints)
        {
            float dist = Vector3.Distance(transform.position, wp.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = wp;
            }
        }

        return closest;
    }

    void UpdateAnimator(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
        {
            float absX = Mathf.Abs(moveDir.x);
            float absY = Mathf.Abs(moveDir.y);

            if (absX > absY)
            {
                anim.SetFloat("moveX", moveDir.x);
                anim.SetFloat("moveY", 0);
                spriteRenderer.flipX = moveDir.x > 0;
            }
            else
            {
                anim.SetFloat("moveX", 0);
                anim.SetFloat("moveY", moveDir.y);
            }
        }
        else
        {
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
        }
    }

    void ReachEndPoint()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.ChangeHealth(-1);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            isChasing = true;
            MusicManager.Instance.EnemyStartedChasing();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
            player = null;
            MusicManager.Instance.EnemyStoppedChasing();
        }
    }
}

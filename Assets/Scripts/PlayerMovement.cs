using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public Rigidbody2D rb;
    public Animator animator;
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        

        animator.SetFloat("Horizontal", Math.abs(horizontal));
        animator.SetFloat("Vertical", Math.abs(vertical));
        rb.velocity = new Vector2(horizontal, vertical) * speed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public Rigidbody2D rb;
    public Animator animator;

    public PlayerCombant playerCombant;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            
            playerCombant.Attack();
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("Vertical", Math.Abs(vertical));
        rb.velocity = new Vector2(horizontal, vertical) * speed;
    }
}

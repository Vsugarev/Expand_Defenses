using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    private Vector2 aimDirection = Vector2.right;

    private float shootCooldown = .5f;
    private float shooterTimer;
    // Update is called once per frame
    void Update()
    {
        shooterTimer -= Time.deltaTime;

        HandleAming();


        if (Input.GetKeyDown(KeyCode.E) && shooterTimer <= 0)
        {
            Shoot();
        }
    }



    public void HandleAming()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shooterTimer = shootCooldown;
    }
    
}

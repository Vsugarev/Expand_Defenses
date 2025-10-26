using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerCombant : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask enemyLayer;
    public int damage = 1;
    public Animator anim;
    public float cooldown = 1;
    private float timer;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("IsAttacking", true);

           
            timer = cooldown;
        }
    }
    
    public void DealDamage()
    {
         Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-damage);
            }
    }
    public void FinishAttack()
    {
        anim.SetBool("IsAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, weaponRange);
    }
}

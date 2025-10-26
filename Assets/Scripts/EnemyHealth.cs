using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida actual: {currentHealth}");

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} se destruye");
            Destroy(gameObject);
        }
    }
}

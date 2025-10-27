using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 5;
    public Text healtText;
    void Start()
    {
        currentHealth = maxHealth;
        healtText.text = "Hp: " + currentHealth + " / " + maxHealth;
    }

 
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healtText.text = "Hp: " + currentHealth + " / " + maxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }


    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}

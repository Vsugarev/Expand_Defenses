using UnityEngine;

public class PlayerHealthShop : MonoBehaviour
{
    public int healthPerCoin = 1;
    public int maxHealth = 10;
    private int currentHealth = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // por ejemplo H para comprar vida
        {
            BuyHealth(1); 
        }
    }

    public void BuyHealth(int amount)
    {
        if (PlayerCurrency.Instance.SpendCoins(amount))
        {
            currentHealth += amount * healthPerCoin;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            Debug.Log("Vida comprada: " + currentHealth);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas");
        }
    }
}

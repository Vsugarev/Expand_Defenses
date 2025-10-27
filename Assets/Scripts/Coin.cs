using UnityEngine;

public class Coin : MonoBehaviour
{
    public float pickupRadius = 0.5f; // radio para recoger la moneda
    public float moveSpeed = 5f;      // velocidad hacia el jugador (opcional)
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // movimiento hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // si ya está cerca del jugador, recoger
            if (Vector3.Distance(transform.position, player.position) < pickupRadius)
            {
                PlayerCurrency.Instance.AddCoins(1);
                Destroy(gameObject);
            }
        }
    }
}

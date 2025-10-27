using UnityEngine;

public class TowerConvertToPlayer : MonoBehaviour
{
    public float countdown = 10f; // Tiempo hasta que pueda moverse

    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false; // Inicialmente no se mueve
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && movement != null)
        {
            movement.enabled = true; // Ahora el jugador puede moverse
            Destroy(this); // Ya no necesitamos este script
        }
    }
}

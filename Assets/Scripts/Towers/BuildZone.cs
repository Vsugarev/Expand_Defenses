using UnityEngine;

public class BuildZone : MonoBehaviour
{
    public GameObject towerPrefab; // Prefab de la torre a construir
    public float buildDistance = 2f; // Distancia mínima para construir

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.LogError("No se encontró el jugador con tag 'Player'");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= buildDistance && Input.GetKeyDown(KeyCode.E))
        {
            BuildTower();
        }
    }

    void BuildTower()
    {
        if (towerPrefab != null)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.z = 0f; // asegurarse que se vea en 2D
            GameObject tower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Torre construida! Nombre: " + tower.name + " Posición: " + tower.transform.position);
        }
        else
        {
            Debug.LogError("No hay prefab asignado en la BuildZone");
        }
    }
}

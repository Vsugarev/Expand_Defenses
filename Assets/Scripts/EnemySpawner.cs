using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDynamic : MonoBehaviour
{
    [Header("Enemies & Spawn Points")]
    public GameObject[] enemyPrefabs;       // Prefabs de enemigos
    public Transform[] startPoints;         // Start Points del mapa

    [Header("Spawn Settings")]
    public float initialSpawnInterval = 5f; // Tiempo inicial entre spawns
    public float minSpawnInterval = 1f;     // Mínimo tiempo entre spawns
    public float spawnAcceleration = 0.1f;  // Cuánto se reduce el intervalo cada spawn
    public float spawnOffsetRange = 1f;     // Separación aleatoria de enemigos para que no se amontonen

    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnInterval);

            // Reducir intervalo para que cada vez aparezcan más rápido
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnAcceleration);
        }
    }

    void SpawnEnemy()
    {
        // Elegir Start Point aleatorio
        Transform startPoint = startPoints[Random.Range(0, startPoints.Length)];

        // Elegir enemigo aleatorio
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Crear con un offset aleatorio para que no se amontonen
        Vector3 offset = new Vector3(
            Random.Range(-spawnOffsetRange, spawnOffsetRange),
            Random.Range(-spawnOffsetRange, spawnOffsetRange),
            0);

        GameObject newEnemy = Instantiate(enemyPrefab, startPoint.position + offset, Quaternion.identity);

        
        EnemyPathFollower pathFollower = newEnemy.GetComponent<EnemyPathFollower>();
        if (pathFollower != null)
        {
            
            pathFollower.currentWaypoint = pathFollower.FindClosestWaypoint();

            
            pathFollower.transform.position = startPoint.position + offset;

            
            pathFollower.ChooseNextWaypoint();
        }
    }
}

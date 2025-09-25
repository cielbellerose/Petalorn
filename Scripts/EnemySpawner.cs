using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnTimeMin = 2f;
    public float spawnTimeMax = 6f;
    public float spawnDistance = 2f;
    public GameObject moss;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTimeMin, Random.Range(spawnTimeMin, spawnTimeMax));
    }
    void SpawnEnemies()
    {
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(moss.transform.position.x - spawnDistance, 
            moss.transform.position.x + spawnDistance);
        enemyPosition.y = moss.transform.position.y + 0.65f;
        enemyPosition.z = Random.Range(moss.transform.position.z - spawnDistance, 
            moss.transform.position.z + spawnDistance);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation)
            as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}

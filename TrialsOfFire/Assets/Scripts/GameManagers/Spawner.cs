using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime, maxSpawnTime;
    public Transform spawnPosition;
    public bool activated;

    // Start is called before the first frame update
    void Start()
    {
        if (activated)
        {
            Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        activated = true;
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float random = Random.Range(minSpawnTime, maxSpawnTime);
            
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(random);
        }
    }

}

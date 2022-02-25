using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private Transform playerPosition;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private Transform[] spawnPoints;  

    [SerializeField]public float timeBtwSpawn = 3f; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeBtwSpawn -= Time.deltaTime;
        if(timeBtwSpawn <= 0)
        {
            timeBtwSpawn = 3f;
            SpawnEnemyWave(2);
        }
    }

    void FixedUpdate(){
       playerPosition = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, 3)].position, enemyPrefab.transform.rotation);
        }
    }
}

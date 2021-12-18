using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject slowEnemy;
    [SerializeField] GameObject fastEnemy;

    int enemyType;
    void Start()
    {
        enemyType = 0;
        StartCoroutine(SpawnContinuously());
    }

    IEnumerator SpawnContinuously()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(8f);
        }
    }

    void SpawnEnemy()
    {
        var enemy = slowEnemy;
        if (enemyType == 0)
        {
            enemy = fastEnemy;
        }
        Instantiate(enemy, transform.position, transform.rotation);
        enemyType = (enemyType + 1) % 2;
    }
}

using System.Collections;
using UnityEngine;

public class EnemySpaw : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject swarmEnemyPrefab;

    public float timeSpawn = 3f;

    void Start()
    {
        StartCoroutine(SpawnEnemy(timeSpawn, swarmEnemyPrefab));

    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(timeSpawn);
        Instantiate(swarmEnemyPrefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}

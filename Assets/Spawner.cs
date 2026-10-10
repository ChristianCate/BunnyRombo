using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] Waypoints1;
    public Transform[] Waypoints2;
    public Transform[] Waypoints3;

 
    public float spawnDelay = 1f;

    public void SpawnWave(int enemyCount)
    {
        StartCoroutine(SpawnEnemies(enemyCount));
    }

    private IEnumerator SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int laneIndex = Random.Range(0, 3);

            Transform[] selectedLane;

            if (laneIndex == 0)
            {
                selectedLane = Waypoints1;
            }
            else if (laneIndex == 1)
            {
                selectedLane = Waypoints2;
            }
            else
            {
                selectedLane = Waypoints3;
            }

            SpawnEnemy(selectedLane);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy(Transform[] lane)
    {

        GameObject enemy = Instantiate( enemyPrefab, lane[0].position, Quaternion.identity);

        SnakeMove movement = enemy.GetComponent<SnakeMove>();

        if (movement != null)
        {
            movement.waypoints = lane;
        }
    }
}
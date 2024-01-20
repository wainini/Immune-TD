using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> waypointsParents;
    [SerializeField] private List<GameObject> enemies;

    private List<List<Transform>> waypoints;

    private List<Wound> spawnPos = new List<Wound>();

    private void Awake()
    {
        waypoints = new List<List<Transform>>();
        foreach(Transform t in waypointsParents)
        {
            List<Transform> waypoints = t.GetComponentsInChildren<Transform>().ToList();
            waypoints.RemoveAt(0);

            this.waypoints.Add(waypoints);
        }
    }

    public void AddSpawnPoint(Wound wound)
    {
        spawnPos.Add(wound);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnEnemySpawnTimer += SpawnRandomEnemy;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnEnemySpawnTimer -= SpawnRandomEnemy;
    }

    public void SpawnRandomEnemy()
    {
        CheckAllSpawnPos();
        if(spawnPos.Count == 0)
        {
            return;
        }

        int random = Random.Range(0,enemies.Count);
        Enemy enemy = Instantiate(enemies[random]).GetComponentInChildren<Enemy>();
   
        random = Random.Range(0, spawnPos.Count);
        enemy.SetPosition(spawnPos[random].transform.position);
        enemy.SetWaypoints(waypoints[random]);
    }

    private void CheckAllSpawnPos()
    {
        foreach(Wound w in spawnPos.ToArray())
        {
            if(w.WoundStage >= 3)
            {
                spawnPos.Remove(w);
            }
        }
    }
}

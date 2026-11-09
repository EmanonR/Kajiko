using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject policePrefab;
    public int defaultMaxSpawnCount = 10;

    public float spawnRangeLimiter = 15f;
    public List<Transform> spawnPoints;

    private void Update()
    {
        if (GameManager.Instance.GAMEOVER) return;
        if (GameManager.Instance.paused) return;

        int adjustedSpawnCount = defaultMaxSpawnCount + (int)(GameManager.Instance.timeSinceAlert / 30);

        if (EnemyTree.enemiesInScene.Count >= adjustedSpawnCount) return;


        if (GameManager.Instance.timeSinceAlert >= 10f)
        {
            Transform spawnPoint = GetSpawnPoint();

            if (spawnPoint == null) return;

            GameObject police = Instantiate(policePrefab, spawnPoint.position, Quaternion.identity);

            police.GetComponent<NavMeshAgent>().enabled = true;
            BehaviorGraphAgent agent = police.GetComponent<BehaviorGraphAgent>();
            agent.enabled = true;
            agent.Init();
            agent.SetVariableValue("Player", GameManager.Instance.player.transform);
        }
    }

    Transform GetSpawnPoint()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        List<Transform> valid = new();

        foreach (Transform point in spawnPoints)
        {
            if (Vector3.Distance(playerPos, point.position) > spawnRangeLimiter) 
                valid.Add(point);
        }

        if (valid.Count == 0) return null;

        return valid[Random.Range(0, valid.Count)];
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints.Count == 0) return;

        foreach (var spawnPoint in spawnPoints)
        {
            if (GameManager.Instance != null)
            {
                float dist = Vector3.Distance(GameManager.Instance.player.transform.position, spawnPoint.position);

                if (dist <= spawnRangeLimiter)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }

            Gizmos.DrawWireSphere(spawnPoint.position, spawnRangeLimiter);
        }
    }
}

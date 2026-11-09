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
        if (GameManager.singleton.GAMEOVER) return;
        if (GameManager.singleton.paused) return;

        int adjustedSpawnCount = defaultMaxSpawnCount + (int)(GameManager.singleton.timeSinceAlert / 30);

        if (EnemyTree.enemiesInScene.Count >= adjustedSpawnCount) return;


        if (GameManager.singleton.timeSinceAlert >= 10f)
        {
            GameObject police = Instantiate(policePrefab);

            Transform selectedSpawnPoint = null;

            while (selectedSpawnPoint == null)
            {
                int i = Random.Range(0, spawnPoints.Count);

                Transform testingTransform = spawnPoints[i];

                float dist = Vector3.Distance(GameManager.singleton.player.transform.position, testingTransform.position);

                if (dist > spawnRangeLimiter)
                    selectedSpawnPoint = testingTransform;
            }

            police.transform.position = selectedSpawnPoint.position;

            police.GetComponent<NavMeshAgent>().enabled = true;
            police.GetComponent<BehaviorGraphAgent>().enabled = true;
            police.GetComponent<BehaviorGraphAgent>().SetVariableValue("Player", GameManager.singleton.player.transform);
            police.GetComponent<BehaviorGraphAgent>().Init();
            police.GetComponent<BehaviorGraphAgent>().SetVariableValue("Player", GameManager.singleton.player.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints.Count == 0) return;

        foreach (var spawnPoint in spawnPoints)
        {
            if (GameManager.singleton != null)
            {
                float dist = Vector3.Distance(GameManager.singleton.player.transform.position, spawnPoint.position);

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

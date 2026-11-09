using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.Behavior;
using System.Linq;

[DefaultExecutionOrder(1000)]
public class BootLoad : MonoBehaviour
{
    List<NPC> enemies = new();

    private void Awake()
    {
        enemies = FindObjectsByType<NPC>().ToList();
    }

    private void Start()
    {
        foreach (NPC enemy in enemies)
        {
            enemy.TryGetComponent(out NavMeshAgent navAgent);
            if (navAgent != null)
                navAgent.enabled = true;

            enemy.TryGetComponent(out BehaviorGraphAgent beAgent);
            if (beAgent != null)
                beAgent.enabled = true;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : State
{
    NavMeshAgent Agent;
    public State attackState;

    public float timer;

    private void Awake()
    {
        Agent = GetComponentInParent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        if (timer < .2f)
        {
            timer += Time.deltaTime;
            return this;
        }

        timer = 0;

        Vector3 playerPos = GetComponentInParent<NPC>().player.position;
        float dist = Vector3.Distance(transform.position, playerPos);

        Agent.destination = playerPos;

        if (dist <= GetComponentInParent<NPC>().attackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
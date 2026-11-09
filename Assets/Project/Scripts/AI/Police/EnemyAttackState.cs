using UnityEngine;

public class EnemyAttackState : State
{
    public State chaseState;

    public override State RunCurrentState()
    {
        Vector3 playerPos = GetComponentInParent<NPC>().player.position;
        float dist = Vector3.Distance(transform.position, playerPos);

        if (dist > GetComponentInParent<NPC>().attackRange)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}

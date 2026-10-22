using UnityEngine;
using UnityEngine.AI;

public class EnemyFleeState : State
{
    public State hideState;
    public string defaultRunName = "Run";

    NavMeshAgent agent;
    HidingSpot targetHidingSpot = null;
    bool destinationFound = false;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        PlayAnimation(defaultRunName);

        if (targetHidingSpot == null)
        {
            FindHidingSpot();

            if (targetHidingSpot != null)
                agent.destination = targetHidingSpot.transform.position;
        }
        
        float dist = Vector3.Distance(transform.position, targetHidingSpot.transform.position);

        if (dist <= GetComponentInParent<Enemy>().interactRange)
            return hideState;
        else
            return this;
    }

    void FindHidingSpot()
    {
        HidingSpot closestHidingSpot = null;

        //Find closest hiding spot
        foreach (HidingSpot hidingSpot in HidingSpot.hidingSpots)
        {
            if (closestHidingSpot == null)
                closestHidingSpot = hidingSpot;
            else
            {
                float currentDist = Vector3.Distance(transform.position, closestHidingSpot.transform.position);
                float newDist = Vector3.Distance(transform.position, hidingSpot.transform.position);

                if (newDist < currentDist)
                    closestHidingSpot = hidingSpot;
            }
        }

        targetHidingSpot = closestHidingSpot;
    }
}
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : State
{
    public State onSightState = null;
    public string defaultIdleName = "Idle";
    public string defaultRoamName = "Walk";
    public bool roamable = true;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    NavMeshAgent agent;
    float timeTillRoam;
    Vector3 targetDest = Vector3.zero;

    public MovementState movementState;
    public enum MovementState
    {
        Idle,
        Walk,
        Flee
    }


    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        timeTillRoam = Random.Range(5, 15);
        agent.speed = walkSpeed;
    }


    public override State RunCurrentState()
    {
        switch (movementState)
        {
            case MovementState.Idle:
                HandleIdle();
                break;
            case MovementState.Walk:
                HandleWalk();
                break;
            case MovementState.Flee:
                HandleFlee();
                break;
            default:
                break;
        }

       

        Vector3 playerPos = GetComponentInParent<Enemy>().player.position;
        float dist = Vector3.Distance(transform.position, playerPos);

        if (dist <= GetComponentInParent<Enemy>().visionRange)
        {
            movementState = MovementState.Flee;
            agent.speed = runSpeed;
            GameManager.singleton.policeAlerted = true;
            return onSightState;
        }
        else
        {
            return this;
        }
    }

    void HandleIdle()
    {
        PlayAnimation(defaultIdleName);

        if (roamable && timeTillRoam > 0)
            timeTillRoam -= Time.deltaTime;

        if (timeTillRoam <= 0)
        {

            if ((!agent.pathPending) && (agent.pathStatus == NavMeshPathStatus.PathInvalid || agent.remainingDistance == Mathf.Infinity) || targetDest == Vector3.zero)
            {
                targetDest = FindNewLocation(25f);
            }

            if (targetDest != Vector3.zero)
            {
                agent.SetDestination(targetDest);
                movementState = MovementState.Walk;
            }
        }
    }
    void HandleWalk()
    {
        PlayAnimation(defaultRoamName);
        float dist = Vector3.Distance(transform.position, targetDest);

        if (dist <= 1f)
        {
            targetDest = Vector3.zero;
            timeTillRoam = Random.Range(5, 15);
            movementState = MovementState.Idle;
        }
    }
    void HandleFlee()
    {

    }

    public Vector3 FindNewLocation(float distance)
    {
        Vector3 dir = Random.insideUnitSphere * distance;

        dir += transform.position;

        NavMesh.SamplePosition(dir, out NavMeshHit hit, distance, NavMesh.AllAreas);

        return hit.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (targetDest != Vector3.zero)
            Gizmos.DrawSphere(targetDest, 1f);
    }
}
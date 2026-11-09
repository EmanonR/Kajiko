using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTree : Enemy
{
    public float turnSpeed = 1f;
    public float bulletDamage = 5f;
    public GameObject bullet;
    public Transform muzzle;
    public Animator animator;
    public AudioSource bulletSource;

    float turnSmoothVel;
    NavMeshAgent agent;

    public static List<EnemyTree> enemiesInScene = new();

    private void Awake()
    {
        player = GameManager.singleton.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemiesInScene.Add(this);
    }

    private void OnDestroy()
    {
        enemiesInScene.Remove(this);
    }

    public void LerpAnimationFloat(string name, float value, float time)
    {
        float t = animator.GetFloat(name);

        float mod = Time.deltaTime * time;

        if (t > value) mod *= -1;

        t = Mathf.Lerp(mod, t, value);
        animator.SetFloat(name, t);
    }

    public void AnimationFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }

    public float GetHP()
    {
        return currentHP;
    }

    public void MoveWithoutRotation(Vector3 dest)
    {
        agent.updateRotation = false;
        agent.destination = dest;
    }

    public Vector3 GetFlankOfTransform(Transform target, float distance)
    {
        Vector3 tryPos = target.position + (-target.forward * distance);
        NavMesh.SamplePosition(tryPos, out NavMeshHit hit, 100, NavMesh.AllAreas);

        return hit.position;
    }

    public Vector3 GetClosestSafePoint(float range)
    {
        Vector3 tryPos = transform.position + ((transform.position - player.transform.position).normalized * range);
        NavMesh.SamplePosition(tryPos, out NavMeshHit hit, 100, NavMesh.AllAreas);

        return hit.position;
    }

    public void SetMoveTarget(Vector3 position)
    {
        agent.destination = position;
    }

    public void RotateToTargetByTime(Transform target)
    {
        Vector3 Dir = (target.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(Dir.x, Dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void Shoot()
    {
        bulletSource.Play();
        Transform nBullet = Instantiate(bullet, muzzle.position, Quaternion.identity).transform;

        nBullet.LookAt(nBullet.position + muzzle.forward);
        nBullet.GetComponent<BulletScript>().damage = bulletDamage;
    }

    public float GetDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public Transform GetPlayerFromGameManager()
    {
        return GameManager.singleton.player.transform;
    }
}

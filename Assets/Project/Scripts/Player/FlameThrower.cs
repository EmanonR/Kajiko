using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerAnimationController))]

[RequireComponent(typeof(FlameThrowerFuel))]
public class FlameThrower : MonoBehaviour
{

    [Header("Stats")]
    public float damagePerSecond = 50f;
    public float fuelPerSecond = 5f;

    [Header("Setup")]
    public ParticleSystem flameParticles;
    public Transform cam;

    [Header("Raycast")]
    public float rayDistance;
    public float raySize;
    public LayerMask rayMask;
    [SerializeField] float maxPitch = 45f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flameClip;
    public AudioClip flameEndClip;

    public static event Action<bool> OnAim;
    public static event Action<bool> OnShoot;

    bool shooting;
    bool aiming;
    bool reloading;
    PlayerAnimationController animController;
    FlameThrowerFuel fuel;

    RaycastHit[] hits = new RaycastHit[16];

    private void Awake()
    {
        shooting = false;
        aiming = false;
        flameParticles.Stop();
        animController = GetComponent<PlayerAnimationController>();
        fuel = GetComponent<FlameThrowerFuel>();

        GameManager.OnGameOver += StopFlame;
        GameManager.OnPause += StopFlame;
        GameManager.OnCardSelection += StopFlame;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= StopFlame;
        GameManager.OnPause -= StopFlame;
        GameManager.OnCardSelection -= StopFlame;
    }

    void Start()
    {
        audioSource.clip = flameEndClip; 
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing) return;

        HandleShoot();
        HandleAim();
        HandleReload();
    }

    void HandleShoot()
    {
        if (reloading) return;

        if (fuel.currentFuel > 0 && KeyBinds.Instance.shootInput.IsPressed())
        {
            if (!shooting)
            {
                flameParticles.Play();
                OnShoot?.Invoke(true);

                if (audioSource.clip != flameClip)
                {
                    audioSource.clip = flameClip;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            
            Shoot();
            shooting = true;
        }
        else
        {
            StopShooting();
        }
    }

    void HandleAim()
    {
        if (reloading) return;

        if (KeyBinds.Instance.aimInput.IsPressed())
        {
            if (!aiming)
                OnAim?.Invoke(true);

            aiming = true;
        }
        else
        {
            if (aiming)
                OnAim?.Invoke(false);

            aiming = false;
        }
    }

    void HandleReload()
    {
        if (reloading) return;

        if (fuel.currentFuel == fuel.maxFuel) return;

        if (KeyBinds.Instance.reloadInput.IsPressed())
        {
            StartCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime()
    {
        reloading = true;
        StopFlame();
        animController.PlayAnimation("Reload");
        animController.animator.SetFloat("ReloadSpeed", PlayerStats.reloadSpeed.GetCalculated());

        yield return null;
        yield return new WaitForSeconds(animController.GetClipLength(1));

        fuel.Refill();

        reloading = false;
    }

    void StopShooting()
    {
        if (shooting)
        {
            flameParticles.Stop();
            OnShoot?.Invoke(false);
            audioSource.clip = flameEndClip;
            audioSource.loop = false;
            audioSource.Play();
        }

        shooting = false;
    }

    void StopFlame()
    {
        StopShooting();

        if (aiming) OnAim?.Invoke(false);
        aiming = false;
    }

    void Shoot()
    {
        Vector3 dir = GetAimDirection();
        Vector3 origin = transform.position - dir * raySize;

        int hitCount = Physics.SphereCastNonAlloc(origin, raySize, dir, hits, rayDistance, rayMask);

        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.TryGetComponent(out NPC enemy))
            {
                enemy.TakeDamage(damagePerSecond * PlayerStats.fireDamage.GetCalculated() * Time.deltaTime);
            }
        }

        fuel.Consume(fuelPerSecond * Time.deltaTime);
    }

    Vector3 GetAimDirection()
    {
        float pitch = Mathf.Clamp(Mathf.DeltaAngle(0f, cam.localEulerAngles.x), -maxPitch, maxPitch);
        return Quaternion.AngleAxis(pitch, transform.right) * transform.forward;
    }

    private void OnDrawGizmosSelected()
    {
        if (cam == null) return;

        Vector3 dir = GetAimDirection();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir * rayDistance);
        Gizmos.DrawWireSphere(transform.position - dir * raySize, raySize);
        Gizmos.DrawWireSphere(transform.position + dir * rayDistance, raySize);
    }
}

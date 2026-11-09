using System;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    [Header("Stats")]
    public float DPS = 50f;
    public float FPS = 5f;
    public float maxAmmo = 100;
    public float currentAmmo;

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

    public static event Action<bool> onAim;
    public static event Action<bool> onShoot;

    bool shooting;
    bool aiming;

    private void Awake()
    {
        shooting = false;
        aiming = false;
        flameParticles.Stop();
        GameManager.onGameOver += StopFlame;
    }

    private void OnDestroy()
    {
        GameManager.onGameOver -= StopFlame;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource.clip = flameEndClip; 
    }

    private void Update()
    {
        if (GameManager.Instance.GAMEOVER) return;
        if (GameManager.Instance.paused) return;

        HandleShoot();
        HandleAim();
        HandleReload();
    }

    void HandleShoot()
    {
        if (currentAmmo > 0 && KeyBinds.Instance.shootInput.IsPressed())
        {
            if (!shooting)
            {
                flameParticles.Play();
                onShoot?.Invoke(true);

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
        if (KeyBinds.Instance.aimInput.IsPressed())
        {
            if (!aiming)
                onAim?.Invoke(true);

            aiming = true;
        }
        else
        {
            if (aiming)
                onAim?.Invoke(false);

            aiming = false;
        }
    }

    void HandleReload()
    {

        if (KeyBinds.Instance.reloadInput.IsPressed())
        {
            currentAmmo = maxAmmo;
            HUD.Instance.UpdateAmmoUI(currentAmmo, maxAmmo);
        }
    }

    void StopShooting()
    {
        if (shooting)
        {
            flameParticles.Stop();
            onShoot?.Invoke(false);
            audioSource.clip = flameEndClip;
            audioSource.loop = false;
            audioSource.Play();
        }

        shooting = false;
    }

    void StopFlame()
    {
        StopShooting();

        if (aiming) onAim?.Invoke(false);
        aiming = false;
    }

    void Shoot()
    {
        Vector3 dir = GetAimDirection();
        Vector3 origin = transform.position - dir * raySize;

        RaycastHit[] hits = Physics.SphereCastAll(origin, raySize, dir, rayDistance, rayMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out NPC enemy))
            {
                enemy.TakeDamage(DPS * Time.deltaTime);
                ScoreManager.AddScore(DPS * Time.deltaTime / 2);

                print("Hit " + enemy.name);
            }
        }

        currentAmmo -= FPS * Time.deltaTime;
        HUD.Instance.UpdateAmmoUI(currentAmmo, maxAmmo);
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

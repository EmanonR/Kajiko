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

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource.clip = flameEndClip;
    }

    private void Update()
    {
        if (GameManager.singleton.GAMEOVER) return;
        if (GameManager.singleton.paused) return;

        if (currentAmmo > 0 && KeyBinds.Instance.shootInput.ReadValue<float>() > .5f)
        {
            if (currentAmmo <= 0) return;

            flameParticles.Play();
            Shoot();
            onShoot?.Invoke(true);

            if (audioSource.clip != flameClip)
            {
                audioSource.clip = flameClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            flameParticles.Stop();
            onShoot?.Invoke(false);

            if (audioSource.clip != flameEndClip)
            {
                audioSource.clip = flameEndClip;
                audioSource.loop = false;
                audioSource.Play();
            }
        }

        if (KeyBinds.Instance.aimInput.ReadValue<float>() > .5f)
        {
            onAim?.Invoke(true);
        }
        else
        {
            onAim?.Invoke(false);
        }

        if (KeyBinds.Instance.reloadInput.ReadValue<float>() > .5f)
        {
            currentAmmo = maxAmmo;
            CanvasManager.singleton.UpdateAmmoUI(currentAmmo, maxAmmo);
        }
    }

    void Shoot()
    {
        Vector3 dir = GetAimDirection();
        Vector3 origin = transform.position - dir * raySize;

        RaycastHit[] hits = Physics.SphereCastAll(origin, raySize, dir, rayDistance, rayMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(DPS * Time.deltaTime);
                ScoreManager.AddScore(DPS * Time.deltaTime / 2);

                print("Hit " + enemy.name);
            }
        }

        currentAmmo -= FPS * Time.deltaTime;
        CanvasManager.singleton.UpdateAmmoUI(currentAmmo, maxAmmo);
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

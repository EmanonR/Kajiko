using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    public GameObject vignetteEffect;

    Player player;

    private static readonly int HealHash = Animator.StringToHash("Heal");
    private static readonly int DamageHash = Animator.StringToHash("Damage");

    private void Awake()
    {
        player = GameManager.Instance.player;
        player.OnHeal += HealEffect;
        player.OnDamaged += DamageEffect;
    }

    private void OnDestroy()
    {
        player.OnHeal -= HealEffect;
        player.OnDamaged -= DamageEffect;
    }

    void HealEffect(float amount)
    {
        vignetteEffect.GetComponent<Animator>().Play(HealHash, -1, 0f);
    }

    void DamageEffect(float amount)
    {
        vignetteEffect.GetComponent<Animator>().Play(DamageHash, -1, 0f);
    }
}

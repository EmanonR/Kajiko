using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string displayName = "Jhon";
    public float maxHP = 50;

    [HideInInspector]
    public float currentHP = 50;
    protected bool isDead;

    public event Action<float, float> OnHealthChanged;
    public event Action<float> OnHeal;
    public event Action<float> OnDamaged;
    public event Action OnDeath;

    protected virtual void Awake()
    {
        currentHP = maxHP;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }
        
    public virtual void TakeDamage(float incoming)
    {
        if (isDead) return;

        currentHP = Mathf.Max(currentHP - incoming, 0);

        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            isDead = true;
            OnDeath?.Invoke();
            Death();
        }
        OnDamaged?.Invoke(incoming);
    }

    public void Heal(float incoming)
    {
        currentHP = Mathf.Clamp(currentHP + incoming, 0, maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
        OnHeal?.Invoke(incoming);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}

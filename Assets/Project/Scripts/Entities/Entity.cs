using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string displayName = "Jhon";
    public float maxHP = 50;

    [HideInInspector]
    public float currentHP = 50;
    bool isDead;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    protected virtual void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float incoming)
    {
        if (isDead) return;

        currentHP -= incoming;

        if (currentHP <= 0 ) 
            Death();

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void Heal(float incoming)
    {
        currentHP = Mathf.Clamp(currentHP + incoming, 0, maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public virtual void Death()
    {
        OnDeath?.Invoke();
        isDead = true;
        Destroy(gameObject);
    }
}

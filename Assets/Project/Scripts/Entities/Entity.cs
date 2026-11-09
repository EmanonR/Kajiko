using UnityEngine;

public class Entity : MonoBehaviour
{
    public new string name = "Jhon";
    public float maxHP = 50;

    [HideInInspector]
    public float currentHP = 50;
    bool isDead;

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
    }

    public void Heal(float incoming)
    {
        currentHP = Mathf.Clamp(currentHP + incoming, 0, maxHP);
    }

    public virtual void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}

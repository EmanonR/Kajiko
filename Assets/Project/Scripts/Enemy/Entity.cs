using UnityEngine;

public class Entity : MonoBehaviour
{
    public new string name = "Jhon";
    public float maxHP = 50;

    [HideInInspector]
    public float currentHP = 50;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float incoming)
    {
        currentHP -= incoming;

        if (currentHP <= 0 ) 
            Death();
    }

    public void Heal(int incoming)
    {
        currentHP = Mathf.Clamp(currentHP + incoming, 0, maxHP);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}

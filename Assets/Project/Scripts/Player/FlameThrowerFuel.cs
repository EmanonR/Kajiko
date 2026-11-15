using System;
using UnityEngine;

public class FlameThrowerFuel : MonoBehaviour
{
    public float maxFuel = 100;
    public float currentFuel;

    public static event Action OnFuelChanged;

    private void Start()
    {
        currentFuel = maxFuel;
    }

    public bool Consume(float amount)
    {
        if (currentFuel < 0)
        {
            return false;
        }

        currentFuel = Mathf.Clamp(currentFuel - amount, 0, maxFuel);
        OnFuelChanged?.Invoke();
        HUD.Instance.UpdateAmmoUI(currentFuel, maxFuel);
        return true;
    }

    public void Refill()
    {
        currentFuel = maxFuel;
        OnFuelChanged?.Invoke();
        HUD.Instance.UpdateAmmoUI(currentFuel, maxFuel);
    }

    public float GetFuel()
    {
        return currentFuel;
    }
}

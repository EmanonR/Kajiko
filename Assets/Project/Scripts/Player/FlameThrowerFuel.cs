using System;
using UnityEngine;

public class FlameThrowerFuel : MonoBehaviour
{
    public float maxFuel = 100;
    public float currentFuel;

    public static event Action<float, float> OnFuelChanged;

    private void Start()
    {
        Refill();
        currentFuel = maxFuel;
    }

    public bool Consume(float amount)
    {
        if (currentFuel <= 0)
            return false;
        

        currentFuel = Mathf.Clamp(currentFuel - amount, 0, maxFuel);
        OnFuelChanged?.Invoke(currentFuel, maxFuel);
        return true;
    }

    public void Refill()
    {
        currentFuel = maxFuel;
        OnFuelChanged?.Invoke(currentFuel, maxFuel);
    }
}

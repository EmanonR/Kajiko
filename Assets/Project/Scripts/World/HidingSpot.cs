using UnityEngine;
using System.Collections.Generic;

public class HidingSpot : MonoBehaviour
{
    public static List<HidingSpot> hidingSpots = new();

    private void Awake()
    {
        hidingSpots.Add(this);
    }

    private void OnDestroy()
    {
        hidingSpots.Remove(this);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PickUpAble : MonoBehaviour
{
    public static List<PickUpAble> pickUpables = new();

    public float pickupProg = 0;

    private void Awake()
    {
        pickUpables.Add(this);
    }

    private void OnDestroy()
    {
        pickUpables.Remove(this);
    }

    public virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : Entity
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        foreach (Interactable interactable in Interactable.interactables)
        {
            float dist = Vector3.Distance(interactable.transform.position, transform.position);

            if (dist <= interactable.interactRange)
            {
                interactable.Interact(this);
            }
        }
    }

    public override void Death()
    {
        //Dont despawn it!
        //base.Death();
    }
}

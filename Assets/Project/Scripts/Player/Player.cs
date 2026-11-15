using UnityEngine;

public class Player : Entity
{
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

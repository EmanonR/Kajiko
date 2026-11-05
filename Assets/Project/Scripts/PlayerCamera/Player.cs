using UnityEngine;

public class Player : Entity
{

    public static bool dead = false;

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

    private void Awake()
    {
        dead = false;
    }

    public override void Death()
    {
        dead = true;

        //Dont despawn it!
        //base.Death();
    }
}

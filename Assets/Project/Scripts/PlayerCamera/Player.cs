using UnityEngine;

public class Player : Entity
{

    public static bool dead = false;
    public float pickUpRange = 10f;

    private void Update()
    {
        foreach (PickUpAble pickUp in PickUpAble.pickUpables)
        {
            float dist = Vector3.Distance(pickUp.transform.position, transform.position);

            if (dist <= pickUpRange)
            {
                if (dist <= 1f)
                {
                    pickUp.OnPickUp(this);
                    continue;
                }

                pickUp.pickupProg += Time.deltaTime / 1f;

                pickUp.transform.position = Vector3.Lerp(pickUp.transform.position, transform.position, pickUp.pickupProg);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}

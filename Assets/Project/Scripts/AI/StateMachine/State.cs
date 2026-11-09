using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();

    public void PlayAnimation(string animName)
    {
        GetComponentInParent<NPCAnimator>().PlayAnimation(animName);
    }
}

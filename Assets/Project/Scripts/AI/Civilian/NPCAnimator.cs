using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    public string currentAnimName;

    public void PlayAnimation(string animName)
    {
        if (animName == currentAnimName) return;

        animator.Play(animName);
        currentAnimName = animName;
    }
}

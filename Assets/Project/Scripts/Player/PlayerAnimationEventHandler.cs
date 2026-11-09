using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    public AudioSource stepSource;

    public void PlayFootStep()
    {
        stepSource.Play();
    }
}

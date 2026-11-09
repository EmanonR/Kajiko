using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioSource stepSource;

    public void PlayFootStep()
    {
        stepSource.Play();
    }
}

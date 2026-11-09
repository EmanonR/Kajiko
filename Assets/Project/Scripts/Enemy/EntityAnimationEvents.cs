using UnityEngine;

public class EntityAnimationEvents : MonoBehaviour
{
    public AudioSource stepSource;

    public void PlayFootStep()
    {
        stepSource.Play();
    }
}

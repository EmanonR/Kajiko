using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioSource stepSource;
    public List<ParticleSystem> reloadParticles = new();
    public ParticleSystem reloadedParticle;

    private void Awake()
    {
        SetReloadParticle(0);
        if (reloadedParticle != null)
        {
            reloadedParticle.Stop();
        }
    }

    public void PlayFootStep()
    {
        stepSource.Play();
    }

    public void SetReloadParticle(int set)
    {
        if (reloadParticles.Count == 0) return;

        foreach (var child in reloadParticles)
        {
            if (set == 1)
            {
                child.Play();
            }
            else
            {
                child.Stop();
            }
        }
    }

    public void Reloaded(float time)
    {
        if (reloadedParticle != null)
            StartCoroutine(PlayReloadedAnim(time));
    }

    public IEnumerator PlayReloadedAnim(float time)
    {
        reloadedParticle.Play();
        yield return new WaitForSeconds(time);
        reloadedParticle.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NL_InteractiveObjectImpact : NL_InteractiveObjectImpactBase
{
    public override void OnImpact(bool _)
    {
        base.OnImpact(_);

        audioSource.clip = impactClips[Random.Range(0, impactClips.Length - 1)];
        audioSource.pitch = Random.Range(soundPitchMinMax.x, soundPitchMinMax.y);
        audioSource.volume = normalizedVelocity;
        audioSource.Play();
    }
}

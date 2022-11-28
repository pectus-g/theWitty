using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class NL_FireSwitch : MonoBehaviour
{
    public bool findComponentsAutomatically = true;
    public bool enableOnAwake = false;
    public Light lightSource;
    public ParticleSystem particleSource;
    public AudioSource audioSource;

    //public float lightEnableSpeed = 1;

    //private float t;
    private float initLightIntensity;

#if UNITY_EDITOR
    void OnEnable()
    {
        if (!findComponentsAutomatically) return;

        lightSource = GetComponentInChildren<Light>();
        particleSource = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();
    }
#endif

    private void Awake()
    {
        if (!Application.isPlaying) return;

        if(lightSource != null)
        {
            initLightIntensity = lightSource.intensity;
        }

        SwitchFire(enableOnAwake);
    }

    /*
    IEnumerator LightEnable()
    {
        while (true)
        {
            if (t < 1)
            {
                t += lightEnableSpeed * Time.deltaTime;
                lightSource.intensity = Mathf.Lerp(0, initLightIntensity, t);
                
            }
            else
            {
                Debug.Log("Stopped");
                StopCoroutine("LightEnable");
            }
            yield return null;
        }
    }
    */

    public void SwitchFire(bool state)
    {
        if (state)
        {
            if (lightSource != null)
            {
                lightSource.enabled = true;

                //t = 0;
                //StartCoroutine("LightEnable");
            }

            if (particleSource != null)
            {
                particleSource.Play();
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (lightSource != null)
            {
                lightSource.enabled = false;
                //lightSource.intensity = 0;
            }

            if (particleSource != null)
            {
                particleSource.Stop();
            }

            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}

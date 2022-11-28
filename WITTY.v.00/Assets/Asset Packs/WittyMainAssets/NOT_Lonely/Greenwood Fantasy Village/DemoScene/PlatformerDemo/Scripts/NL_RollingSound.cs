using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NL_GroundChecker))]
[RequireComponent(typeof(Rigidbody))]
public class NL_RollingSound : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private NL_GroundChecker groundChecker;

    public float maxSpeed = 6;

    [Range(0, 1)]
    public float maxVolume = 1;
    public Vector2 pitchMinMax = new Vector2(0.8f, 1.2f);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundChecker = GetComponent<NL_GroundChecker>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0;
        audioSource.Play();
    }

    void Update()
    {
        if (groundChecker.isGrounded)
        {
            float vol = NL_Utilities.Remap(Mathf.Abs(rb.velocity.x), 0, maxSpeed, 0, maxVolume);
            float pitch = NL_Utilities.Remap(Mathf.Abs(rb.velocity.x), 0, maxSpeed, pitchMinMax.x, pitchMinMax.y);
            audioSource.volume = vol;
            audioSource.pitch = pitch;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}

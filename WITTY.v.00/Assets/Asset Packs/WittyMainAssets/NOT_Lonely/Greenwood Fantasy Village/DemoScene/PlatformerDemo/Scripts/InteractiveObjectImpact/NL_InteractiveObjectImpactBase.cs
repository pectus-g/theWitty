using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NL_InteractiveObjectImpactBase : MonoBehaviour
{
    [Tooltip("The maximum hit velocity this object can handle. If hit velocity is higher than this value, then object will break.")]
    public float maxVelocity = 10;

    [Header("IMPACT PROPERTIES")]
    public float impactVelocityThreshold = 6f;
    public AudioClip[] impactClips;

    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public float normalizedVelocity;
    [HideInInspector] public Collision impactColl;
    [HideInInspector] public Vector3 impactPoint;
    
    [Header("SOUND PROPERTIES")]
    public Vector2 soundMinMaxDistance = new Vector2(1, 500);
    public Vector2 soundPitchMinMax = new Vector2(0.9f, 1);
    public float hitVolumeMultiplier = 1.5f;
    public float breakVolumeMultiplier = 1.5f;

    [Header("BREAK PROPERTIES")]
    [Tooltip("Objects on these layers can break this object if their hit velocity more than 'Max Velocity' value.")]
    public LayerMask hitByLayers = ~0;
    [Tooltip("Objects on these layers will be treated as dead zones. \nCurrent object will break if collide with these objects even if their hit velocity is lower than the 'Max Velocity' value.")]
    public LayerMask deadZoneLayer;
    public float explosionForceMultiplier = 1;

    private bool canExplode = false;

    public virtual void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1;
        audioSource.minDistance = soundMinMaxDistance.x;
        audioSource.maxDistance = soundMinMaxDistance.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canExplode) return;

        float velocity = collision.relativeVelocity.magnitude;
        canExplode = velocity >= maxVelocity;

        normalizedVelocity = NL_Utilities.Remap(velocity, 0, maxVelocity, 0, 1);

        impactColl = collision;
        impactPoint = collision.GetContact(0).point;

        if ((deadZoneLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            canExplode = true;
            normalizedVelocity = 1;
            OnImpact(true);
            return;
        }
        if((hitByLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            canExplode = false;
            return;
        }

        if (!canExplode)
        {
            if (velocity < impactVelocityThreshold) return;
        }

        OnImpact(canExplode);  
    }

    public virtual void OnImpact(bool explode)
    {

    }
}

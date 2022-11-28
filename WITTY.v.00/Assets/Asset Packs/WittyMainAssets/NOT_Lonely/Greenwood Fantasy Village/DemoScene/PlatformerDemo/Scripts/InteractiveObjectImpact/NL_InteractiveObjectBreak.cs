using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NL_InteractiveObjectBreak : NL_InteractiveObjectImpactBase
{
    [Tooltip("If true this object will emit an explosion wave which can hit objects around.")]
    public bool explosive = false;
    public float explosionRadius = 1.5f;
    public float explosionForceMax = 0.5f;
    public float piecesMass = 0.1f;
    [Space(5)]
    [Tooltip("How much time should pass before break the object.")]
    public float breakDelay = 0;
    [Tooltip("How much time should pass before destroy the broken object pieces. \nIf set to 0, then the pieces will never be destroyed.")]
    public float destroyPiecesDelay = 0;
    [Tooltip("Should the root object have to be destroyed as well including its children?")]
    public bool destroyInitialObject = false;

    [Space(5)]
    public AudioClip[] breakClips;
    public MeshRenderer[] unbrokenMeshes;
    public Collider[] unbrokenColliders;
    public MeshRenderer[] brokenMeshes;

    [Header("EVENTS")]
    public UnityEvent onImpact;
    public UnityEvent onPreBreak;
    public UnityEvent onBreak;

    private Rigidbody unbrokenRigidbody;
    private List<Rigidbody> brokenRigidbodies = new List<Rigidbody>();
    private List<Collider> brokenColliders = new List<Collider>();
    private WaitForSeconds breakDelaySeconds;
    private WaitForSeconds destroyPiecesSeconds;
    private bool preBreak = false;

    public override void Awake()
    {
        base.Awake();
        unbrokenRigidbody = GetComponent<Rigidbody>();

        for (int i = 0; i < brokenMeshes.Length; i++)
        {
            Collider coll = brokenMeshes[i].GetComponent<Collider>();

            if (coll != null) brokenColliders.Add(coll);
        }

        breakDelaySeconds = new WaitForSeconds(breakDelay);
        destroyPiecesSeconds = new WaitForSeconds(destroyPiecesDelay);
    }

    public IEnumerator PreBreak()
    {
        if (!preBreak) {

            preBreak = true;
            onPreBreak?.Invoke();
            yield return breakDelaySeconds;
            BreakObject();
        }
    }

    IEnumerator AfterBreak()
    {
        yield return destroyPiecesSeconds;

        for (int i = 0; i < brokenMeshes.Length; i++)
        {
            Destroy(brokenMeshes[i].gameObject);
        }

        if (destroyInitialObject) Destroy(gameObject);
    }

    public override void OnImpact(bool expolide)
    {
        base.OnImpact(expolide);
        if (expolide)
        {
            StartCoroutine(PreBreak());
        }
        else
        {
            SetClip(impactClips);
            PlaySFX(hitVolumeMultiplier);

            onImpact?.Invoke();
        }
    }

    void PlaySFX(float _volumeMultiplier)
    {
        if (audioSource == null) return;

        audioSource.pitch = Random.Range(soundPitchMinMax.x, soundPitchMinMax.y);
        audioSource.volume = Mathf.Clamp01(normalizedVelocity * _volumeMultiplier);
        audioSource.Play();
    }

    void SetClip(AudioClip[] clipsArray)
    {
        if (clipsArray == null) return;
        if (clipsArray.Length == 0) return;
        if (audioSource == null) return;

        audioSource.clip = clipsArray[Random.Range(0, clipsArray.Length)];
    }
    public void BreakObject(float velocityOverride = 0)
    {
        if (!enabled) return;

        onBreak?.Invoke();

        if(velocityOverride != 0) normalizedVelocity = velocityOverride;

        SetClip(breakClips);
        PlaySFX(breakVolumeMultiplier);

        for (int i = 0; i < unbrokenColliders.Length; i++)
        {
            unbrokenColliders[i].enabled = false;
        }
        for (int i = 0; i < unbrokenMeshes.Length; i++)
        {
            unbrokenMeshes[i].enabled = false;
        }

        unbrokenRigidbody.isKinematic = true;
        unbrokenRigidbody.useGravity = false;

        if (explosive)
        {
            Collider[] overlapedColliders = new Collider[100];
            float _expRadius = explosionRadius * 5f;
            if (Physics.OverlapSphereNonAlloc(transform.position, _expRadius, overlapedColliders) > 0)
            {
                for (int i = 0; i < overlapedColliders.Length; i++)
                {
                    if (overlapedColliders[i] != null)
                    {
                        Rigidbody hitRb = overlapedColliders[i].GetComponent<Rigidbody>();
                        if (hitRb != null)
                        {
                            float explosionForce = Mathf.Clamp(explosionForceMultiplier * 200, -explosionForceMax * 20, explosionForceMax * 20);
                            hitRb.AddExplosionForce(explosionForce, transform.position, _expRadius, 5);
                        }

                        float dist = Vector3.Distance(transform.position, overlapedColliders[i].transform.position);

                        if (dist <= explosionRadius)
                        {
                            NL_InteractiveObjectBreak breakableObject = overlapedColliders[i].GetComponent<NL_InteractiveObjectBreak>();
                            if (breakableObject != null)
                            {
                                breakableObject.BreakObject(NL_Utilities.Remap(Vector3.Distance(transform.position, breakableObject.transform.position), 0, explosionRadius, 0, 1));
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < brokenMeshes.Length; i++)
        {
            brokenMeshes[i].enabled = true;

            Rigidbody rb = brokenMeshes[i].gameObject.GetComponent<Rigidbody>();
            if (rb == null) rb = brokenMeshes[i].gameObject.AddComponent<Rigidbody>();

            rb.mass = piecesMass;

            Vector3 forceVector = (rb.transform.position - impactPoint).normalized;
            rb.AddExplosionForce(Mathf.Clamp(explosionForceMultiplier * normalizedVelocity, -explosionForceMax, explosionForceMax), impactPoint, 5, 0, ForceMode.Impulse);
            rb.AddTorque(Mathf.Clamp(normalizedVelocity * explosionForceMultiplier, -explosionForceMax, explosionForceMax) * forceVector, ForceMode.Impulse);

            brokenColliders[i].enabled = true;
        }

        if(destroyPiecesDelay > 0) StartCoroutine(AfterBreak());

        enabled = false;
    }
}

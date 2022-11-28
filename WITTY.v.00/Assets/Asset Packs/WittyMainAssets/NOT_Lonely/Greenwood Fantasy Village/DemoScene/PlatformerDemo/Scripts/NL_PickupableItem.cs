using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NL_PickupableItem : MonoBehaviour
{
    public LayerMask playerLayer;
    public int scoreValue = 1;

    [Header("Components")]
    public Transform moveablePart;
    public Collider trigger;
    public MeshRenderer meshRenderer;
    public AudioSource audioSource;
    public Light lightSource;

    [Header("Animation")]
    public float moveAmplitude = 0.1f;
    public float moveSpeed = 1;
    public float rotationSpeed = 80;

    private NL_Score scoreManager;

    public UnityEvent onPickUp;

    private void Start()
    {
        scoreManager = FindObjectOfType<NL_Score>();
        NL_Score.totalLootAmount += scoreValue;
        scoreManager.UpdateScore(0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if((playerLayer.value & (1 << collider.gameObject.layer)) > 0)
        {
            PickUp();
        }
    }

    private void Update()
    {
        moveablePart.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        float pingPongValue = Mathf.SmoothStep(0, moveAmplitude, Mathf.PingPong(Time.time * moveSpeed, 1));

        moveablePart.localPosition = new Vector3(0, pingPongValue, 0);
    }

    public void PickUp()
    {
        onPickUp?.Invoke();

        if(scoreManager != null) scoreManager.UpdateScore(scoreValue);

        trigger.enabled = false;
        meshRenderer.enabled = false;

        if (audioSource != null) audioSource.Play();
        if (lightSource != null) lightSource.enabled = false;
    }
}

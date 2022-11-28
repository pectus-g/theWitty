using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NL_ItemSpawner : MonoBehaviour
{
    public Transform[] items;
    public bool alwaysHasItem = false;
    public bool spawnAtStart = true;
    public bool spawnDisabled = true;

    private Transform spawnedItem;
    private MeshRenderer[] renderers;
    private Collider[] colliders;
    private Light lightSource;

    public UnityEvent onReveal;

    void Awake()
    {
        if(spawnAtStart) SpawnItem();
        if (spawnDisabled)
        {
            if (spawnedItem == null) return;
            SwitchItem(false);
        }
    }

    public void SpawnItem()
    {
        if (items != null)
        {
            if (items.Length == 0) return;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) return;
            }

            Transform item = null;

            int hasItem = alwaysHasItem ? 1 : Random.Range(0, 2);

            if (hasItem == 1) item = items[Random.Range(0, items.Length)];

            if (item == null) return;

            spawnedItem = Instantiate(item, transform.position, Quaternion.identity, transform);

            renderers = spawnedItem.GetComponentsInChildren<MeshRenderer>();
            colliders = spawnedItem.GetComponentsInChildren<Collider>();
            lightSource = spawnedItem.GetComponent<Light>();
        }
    }

    private void SwitchItem(bool state)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = state;  
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = state;
        }

        if(lightSource != null) lightSource.enabled = state;
    }

    public void RevealItem()
    {
        if (spawnedItem == null) return;

        spawnedItem.parent = null;
        spawnedItem.eulerAngles = Vector3.zero;
        SwitchItem(true);

        onReveal?.Invoke();
    }
}

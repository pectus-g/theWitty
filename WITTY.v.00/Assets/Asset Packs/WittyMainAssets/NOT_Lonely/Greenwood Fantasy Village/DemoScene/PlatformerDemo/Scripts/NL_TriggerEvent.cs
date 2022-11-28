using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NL_TriggerEvent : MonoBehaviour
{
    public Collider[] otherColliders;
    [Space(10)]
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < otherColliders.Length; i++)
        {
            if (other == otherColliders[i])
            {
                onTriggerEnter.Invoke();
            }
        }
    }
}

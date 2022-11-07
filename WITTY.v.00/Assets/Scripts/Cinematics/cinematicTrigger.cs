using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
public class cinematicTrigger : MonoBehaviour
{
    bool alreadyTrggered =false;
    private void OnTriggerEnter(Collider other)
{
    if(!alreadyTrggered && other.gameObject.tag== "Player")
    {
        alreadyTrggered =true;
        GetComponent<PlayableDirector>().Play();
    }
   
}   
}

}


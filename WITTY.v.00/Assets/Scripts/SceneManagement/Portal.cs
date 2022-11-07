using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  enum DestinationIdentifier
  { 
    A, B, C, D, E
  }
    public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad=-1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField] float fadeOutTime=1f;
    [SerializeField] float fadeInTime=2f;
    [SerializeField] float fadeWaitTime=0.5f;

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag =="Player")
    {
        StartCoroutine(Transition());
        //SceneManager.LoadScene(sceneToLoad);
    }
  }
  private IEnumerator Transition()
  {
    if(sceneToLoad <0){
      Debug.LogError("Scene to load not set");
      yield break;
    }

 

    DontDestroyOnLoad(gameObject);

   Fader fader = FindObjectOfType<Fader>();

    yield return fader.FadeOut(fadeOutTime);

    //save current level
    SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
    wrapper.Save();

    yield return SceneManager.LoadSceneAsync(sceneToLoad);

    //load current level
    wrapper.Load();

    Portal otherPortal =GetOtherPortal();
    UpdatePlayer(otherPortal);
    wrapper.Save();
    
    yield return new WaitForSeconds(fadeWaitTime);
    yield return fader.FadeIn(fadeInTime);

    print("Sceneloaded");
    Destroy(gameObject);
  }
  private void UpdatePlayer(Portal otherPortal)
  {
    GameObject player = GameObject.FindWithTag("Player");
   // player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
   player.GetComponent<NavMeshAgent>().enabled =false;
    player.transform.position =otherPortal.spawnPoint.position;
    player.transform.rotation =otherPortal.spawnPoint.rotation;
       player.GetComponent<NavMeshAgent>().enabled =true;

  }
  private Portal GetOtherPortal()
  {
    foreach (Portal portal in FindObjectsOfType<Portal>())
    {if (portal == this) continue;
    if(portal.destination !=destination) continue;
    return portal;
        
    }
    return null;
  }
  
}
}


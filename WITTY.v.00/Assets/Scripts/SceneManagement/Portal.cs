using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Control;

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
    [SerializeField] float fadeOutTime=.5f;
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

    //save current level
    SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

    PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    playerController.enabled = false;
    
    yield return fader.FadeOut(fadeOutTime);
    
    savingWrapper.Save();

    yield return SceneManager.LoadSceneAsync(sceneToLoad);
    PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    newPlayerController.enabled = false;
    
    //load current level
    savingWrapper.Load();

    Portal otherPortal =GetOtherPortal();
    UpdatePlayer(otherPortal);
    savingWrapper.Save();
    
    yield return new WaitForSeconds(fadeWaitTime);
    fader.FadeIn(fadeInTime);

    newPlayerController.enabled = true;
    Destroy(gameObject);
  }
  private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

  private Portal GetOtherPortal()
  {
    foreach (Portal portal in FindObjectsOfType<Portal>())
    {
    if (portal == this) continue;
    if(portal.destination !=destination) continue;
    return portal;
        
    }
    return null;
  }
  
}
}


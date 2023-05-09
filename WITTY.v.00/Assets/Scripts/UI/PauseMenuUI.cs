using RPG.Control;
using UnityEngine;
using RPG.SceneManagement;
namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        PlayerController playerController;

        private void Start() {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Time.timeScale = 0;
            playerController.enabled = false;
        }

        private void OnDisable()
        {  
            if (playerController == null) return;
            Time.timeScale = 1; // debugging purpuses to make faster the time make 5
            playerController.enabled = true;
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            savingWrapper.LoadMenu();
        }
    }
}
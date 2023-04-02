using System;
using System.Collections;
using GameDevTV.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
public class SavingWrapper : MonoBehaviour
{

private const string currentSaveKey = "currentSaveName";
[SerializeField] float fadeInTime=0.1f;
// listen key events
 [SerializeField] float fadeOutTime = 0.1f;
[SerializeField] int firstFieldBuildIndex = 1;

        public void ContinueGame() 
        {
            if(!PlayerPrefs.HasKey(currentSaveKey)) return;
            if(!GetComponent<SavingSystem>().SaveFileExists(GetCurrentSave())) return;
            StartCoroutine(LoadLastScene());
        }
         public void NewGame(string saveFile)
        {
            if(!String.IsNullOrEmpty(saveFile)) return;
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }

        private void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(currentSaveKey, saveFile);
        }

        private string GetCurrentSave()
        {
            return PlayerPrefs.GetString(currentSaveKey);
        }

        private IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(GetCurrentSave());
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator LoadFirstScene()
        {
        Fader fader=FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(firstFieldBuildIndex);
        yield return fader.FadeIn(fadeInTime);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
         if(Input.GetKeyDown(KeyCode.D))
        {
            Delete();
        }
    }
    public void Load()
    {
    GetComponent<SavingSystem>().Load(GetCurrentSave());
    }
     public void Save()
    {
         GetComponent<SavingSystem>().Save(GetCurrentSave());
    }
    private void Delete()
    {
        GetComponent<SavingSystem>().Delete(GetCurrentSave());
    }
}
}

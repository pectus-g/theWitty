using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;

namespace RPG.SceneManagement
{
public class SavingWrapper : MonoBehaviour
{

const string defaultSaveFile = "save";
[SerializeField] float fadeInTime=0.2f;
// listen key events
 [SerializeField] float fadeOutTime = 0.2f;

        public void ContinueGame() 
        {
            StartCoroutine(LoadLastScene());
        }
    private IEnumerator LoadLastScene()
    {
        Fader fader=FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
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
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
     public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
    private void Delete()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
    }
}
}

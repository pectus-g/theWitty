using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
public class SavingWrapper : MonoBehaviour
{

const string defaultSaveFile = "save";
[SerializeField] float fadeInTime=0.2f;
// listen key events
  private void Awake() 
        {
            StartCoroutine(LoadLastScene());
        }
    private IEnumerator LoadLastScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        Fader fader=FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
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

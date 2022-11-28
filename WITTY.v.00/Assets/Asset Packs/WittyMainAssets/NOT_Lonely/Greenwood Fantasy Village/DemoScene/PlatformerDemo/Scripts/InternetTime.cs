using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InternetTime : MonoBehaviour
{
    public Text timerText;
    void Start()
    {
        StartCoroutine("getTime");
    }

    IEnumerator getTime()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://not-lonely.com/webdemo/serverTime.php");
        yield return www.SendWebRequest();

        string result = www.downloadHandler.text;
        if (int.Parse(result) > 0)
        {
            timerText.text = result + " days left!";
            if (result == "1") timerText.text = result + " day left!";
        }
        else
        {
            timerText.enabled = false;
        }
    }
}

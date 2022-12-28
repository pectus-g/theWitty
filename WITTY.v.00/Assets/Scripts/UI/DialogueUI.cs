using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI AITxet;
    [SerializeField] Button nextButton;
    void Start()
    {
        playerConversant=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        nextButton.onClick.AddListener(Next);//onclick is unity event. Unity events needs to Addlistener
       UpdateUI();
    }
void Next()
{
playerConversant.Next();
UpdateUI();
}
   
    void UpdateUI()
    {
         AITxet.text=playerConversant.GetText();
         nextButton.gameObject.SetActive(playerConversant.HasNext());
    }
}
}


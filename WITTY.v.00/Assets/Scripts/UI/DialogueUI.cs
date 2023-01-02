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
    [SerializeField] GameObject AIResponse;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI conversantName;
    void Start()
    {
        playerConversant=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        playerConversant.onConversationUpdated+=UpdateUI;
        nextButton.onClick.AddListener(() => playerConversant.Next());//onclick is unity event. Unity events needs to Addlistener
        quitButton.onClick.AddListener(() => playerConversant.Quit());
       UpdateUI();
    }
void Next()
{
playerConversant.Next();

}
   
    void UpdateUI()
    {
        gameObject.SetActive(playerConversant.IsActive());
         if(!playerConversant.IsActive())
         {
            return;
         }
         conversantName.text=playerConversant.GetCurrentConversantName();
         AIResponse.SetActive(!playerConversant.IsChoosing());
         choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
         if(playerConversant.IsChoosing())
            { //choiceRoot.DetachChildren yerine 
                BuildChoiceList();
            }
            else
         {
            AITxet.text=playerConversant.GetText();
         nextButton.gameObject.SetActive(playerConversant.HasNext());
         }
        
        
    }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button=choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => 
                {
                    playerConversant.SelectChoice(choice);//each time we go through this loop, it's adding a listener to a different buton and each listener is a different dunction that is being constructed on the spot in that foreach loop.

                   
                });
            }
        }
    }
}


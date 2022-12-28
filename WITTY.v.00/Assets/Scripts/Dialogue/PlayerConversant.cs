using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RPG.Dialogue
{
public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    DialogueNode currentNode=null;
    private void Awake() {
        currentNode=currentDialogue.GetRootNode();
    }
    public string GetText()
    {
        if(currentDialogue==null)
        {
            return "";
        }
        return currentNode.GetText();
    }
    public IEnumerable<string> GetChoices()
    {
        yield return "Hello";
         yield return "I remember you!";
    }
    public void Next()
    {
        DialogueNode[] children=currentDialogue.GetAllChildren(currentNode).ToArray();
        int randomIndex=Random.Range(0,children.Count());
        currentNode=children[randomIndex];
    }
    public bool HasNext()
    {
         return currentDialogue.GetAllChildren(currentNode).Count()>0;
    }

}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject {
    [SerializeField]
    List <DialogueNode> nodes;//adding node to the list easier than the array 

    Dictionary<string, DialogueNode> nodeLookup=new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR //everything is in here will only be included when we're running in the editor
    private void Awake() {
        if(nodes.Count ==0)
        {
            nodes.Add(new DialogueNode());
        }
        OnValidate();
    }
#endif
   private void OnValidate() {
        nodeLookup.Clear();
        foreach (DialogueNode node in GetAllNodes())
        {
            nodeLookup[node.uniqueID]=node;
        }
    }
    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return nodes;
    } 
    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }
    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
    {
       // List<DialogueNode> result = new List<DialogueNode>();
        foreach (string childID in parentNode.children)
        {
            if(nodeLookup.ContainsKey(childID))
            {
               yield return nodeLookup[childID];
            }
           

        }
        //return result;
    }
}
}

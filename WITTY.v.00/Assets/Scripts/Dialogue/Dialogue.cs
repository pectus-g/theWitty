using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject {
    [SerializeField]
    List <DialogueNode> nodes;//adding node to the list easier than the array 

#if UNITY_EDITOR //everything is in here will only be included when we're running in the editor
    private void Awake() {
        if(nodes.Count ==0)
        {
            nodes.Add(new DialogueNode());
        }
    }
#endif
    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return nodes;
    } 
    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }
}
}

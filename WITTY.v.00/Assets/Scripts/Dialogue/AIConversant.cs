using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;


namespace RPG.Dialogue
{
public class AIConversant : MonoBehaviour, IRaycastable//when you hover over the npc we want a new cursor type
{
    [SerializeField] Dialogue dialogue=null;
   public CursorType GetCursorType()
   {
        return CursorType.Dialogue;
   }

   public bool HandleRaycast(PlayerController callingController)
   {if(dialogue==null)
   {
    return false;
   }
   if(Input.GetMouseButtonDown(0))
   {
    callingController.GetComponent<PlayerConversant>().StartDialogue(dialogue);
   }
return true;
   }
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
{
    Dialogue selectedDialogue=null;
    

    [MenuItem("Window/Dailogue Editor")]
    public static void ShowEditorWindow()
    {
       GetWindow(typeof(DialogueEditor),false,"Dialogue Editor");
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID,int line)
    {
       Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;//casting for dialogur if it is not dialogue  this will return null
       if(dialogue!=null)
       {
      ShowEditorWindow();
        return true;
       }
        return false;
    }
    private void OnEnable() {
        Selection.selectionChanged +=OnSelectionChanged;
    }
    private void OnSelectionChanged() {// name of function does not matter y
      Dialogue newDialogue= Selection.activeObject as Dialogue;
      if(newDialogue !=null)
      {
        selectedDialogue=newDialogue;
        Repaint();
      }
    }
    private void OnGUI()
    {
        if(selectedDialogue== null)
        {
        EditorGUILayout.LabelField("No Dialogue Selected");
        }
        else 
        {
        foreach (DialogueNode node in selectedDialogue.GetAllNodes())
        {
          EditorGUI.BeginChangeCheck();

          EditorGUILayout.LabelField("Node:");

          string newText = EditorGUILayout.TextField(node.text);
          string newUniqueID = EditorGUILayout.TextField(node.uniqueID);
          
          if(EditorGUI.EndChangeCheck())
          {
            Undo.RecordObject(selectedDialogue,"Update Dialogue Text");

            node.text=newText;
            node.uniqueID=newUniqueID;
           
          }
        }
        }
     
    }
}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
{
    [MenuItem("Window/Dailogue Editor")]
    public static void ShowEditorWindow()
    {
       GetWindow(typeof(DialogueEditor),false,"Dialogue Editor");
    }

    [OnOpenAssetAttribute(1)]
    public static bool OnOpenAsset(int instanceID,int line)
    {
       Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;//casting for dialogur if it is not dialogu this will return null
       if(dialogue!=null)
       {
      ShowEditorWindow();
        return true;
       }
        return false;
    }
}

}

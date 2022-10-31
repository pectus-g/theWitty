
using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StarAction(IAction action)//if you want to attack to an enemy cancel the destination
        {   if(currentAction==action) return;
            if(currentAction!=null)
            {
           currentAction.Cancel();
          
            } 
            currentAction=action;
        }
    }
}
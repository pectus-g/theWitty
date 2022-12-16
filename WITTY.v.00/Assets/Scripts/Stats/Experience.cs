using UnityEngine;
using System;
using GameDevTV.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints=0;

         public event Action onExperienceGained;


        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

       
        public float GetXP()
        {
            return experiencePoints;
        }
        public object CaptureState()
        {

         return experiencePoints;
        }
        public void RestoreState(object state)
        {
         experiencePoints =(float)state;   //casting the sate and put it in healthstore
        
        }
    }
    
}
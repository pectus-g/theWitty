using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        float healthPoints=-1f;
        bool isDead=false;
        private void Start()
        {
            if(healthPoints<0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
            
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints =Mathf.Max(healthPoints - damage,0);
            print(healthPoints);
            if(healthPoints<=0)
            {
                Die();
                AwardExperience(instigator);
            }
        }
        public float GetPercentage()
        {
         return 100*(healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health)) ;   
        }

        private void Die()
        {   if(isDead) return;
            isDead=true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();

        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience=instigator.GetComponent<Experience>();
            if(experience==null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));

        }

        public object CaptureState()
        {
//what you want to save this field will capture but must have serialiblevector 3
         return healthPoints;
        }
        public void RestoreState(object state)
        {
         healthPoints =(float)state;   //casting the sate and put it in healthstore
         if(healthPoints<=0)
            {
                Die();
            }
        }
    }
}
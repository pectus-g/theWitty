using UnityEngine;
using RPG.Saving;
namespace RPG.Core
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float healthPoints=300f;
        bool isDead=false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints =Mathf.Max(healthPoints - damage,0);
            print(healthPoints);
            if(healthPoints==0)
            {
                Die();
            }
        }
        private void Die()
        {   if(isDead) return;
            isDead=true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();

        }
        public object CaptureState()
        {
//what you want to save this field will capture but must have serialiblevector 3
         return healthPoints;
        }
        public void RestoreState(object state)
        {
         healthPoints =(float)state;   //casting the sate and put it in healthstore
         if(healthPoints==0)
            {
                Die();
            }
        }
    }
}
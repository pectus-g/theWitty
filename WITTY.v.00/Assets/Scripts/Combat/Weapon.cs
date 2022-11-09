
using UnityEngine;

namespace RPG.Combat
{
[CreateAssetMenu(fileName="Weapon",menuName="Weapons/Make New Weapon",order=0)]
public class Weapon : ScriptableObject
{       [SerializeField] GameObject weaponPrefab = null;//equipped prefab
      
        [SerializeField] AnimatorOverrideController animatorOverride=null;
        [SerializeField] float weaponDamage=5f;
        [SerializeField] float weaponRange =2f;

        public void Spawn(Transform handTransform,Animator animator)
        {   if(weaponPrefab!= null)
        {
            Instantiate (weaponPrefab,handTransform);
        }
           if(animatorOverride!=null)//unarmed anim works
           {
            animator.runtimeAnimatorController=animatorOverride;
           }
        
        }
        public float GetDamage()
        {
            return weaponDamage;
        }
         public float GetRange()
        {
            return weaponRange;
        }


   
}
}
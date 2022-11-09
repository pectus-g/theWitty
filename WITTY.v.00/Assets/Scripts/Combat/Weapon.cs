
using UnityEngine;

namespace RPG.Combat
{
[CreateAssetMenu(fileName="Weapon",menuName="Weapons/Make New Weapon",order=0)]
public class Weapon : ScriptableObject
{       [SerializeField] GameObject weaponPrefab = null;//equipped prefab
      
        [SerializeField] AnimatorOverrideController animatorOverride=null;
        [SerializeField] float weaponDamage=5f;
        [SerializeField] float weaponRange =2f;
        [SerializeField] bool isRightHanded=true;

        public void Spawn(Transform rightHand,Transform leftHand,Animator animator)
        {   if(weaponPrefab!= null)
        {
            Transform handTransform;
            if(isRightHanded) handTransform=rightHand;
            else handTransform = leftHand;
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
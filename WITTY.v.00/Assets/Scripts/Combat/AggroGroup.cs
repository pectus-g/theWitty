using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class AggroGroup : MonoBehaviour
{
    [SerializeField] Fighter[] fighters;
    [SerializeField] bool activeOnStart=false;
    // Start is called before the first frame update
    void Start()
    {
        Activate(activeOnStart);
    }
    public void Activate(bool shouldActivate)
    {
        foreach (Fighter fighter in fighters)
        {
            CombatTarget target = fighter.GetComponent<CombatTarget>();
            if(target!=null)
            {
                target.enabled=shouldActivate;
            }
            fighter.enabled=shouldActivate;
        }
    }


}
}


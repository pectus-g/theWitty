using UnityEngine;
using System.Collections.Generic;
using System;
namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Demo Targeting", menuName = "Abilities/Targeting/Demo", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
      public override void StartTargeting(GameObject use, Action<IEnumerable<GameObject>> finished)
        {
            Debug.Log("Demo Targeting Started");
            finished(null);
        }
    }
}
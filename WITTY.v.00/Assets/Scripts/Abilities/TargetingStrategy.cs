using UnityEngine;
using System.Collections.Generic;
using System;
namespace RPG.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> finished);
    }
}
using UnityEngine;

namespace RPG.Stats
{
[CreateAssetMenu(fileName="Progression",menuName="Stats/New Progression", order = 0)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses =null;
     public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat!=stat) continue;
                    if(progressionStat.levels.Length < level) continue;
                    return progressionStat.levels[level -1];
                }
                
               
            }
            return 0;
        }

    [System.Serializable]
    class ProgressionCharacterClass
    {   public CharacterClass characterClass;
        public ProgressionStat[] stats;
        //you can add anything that you wnat to improve by level up. Like mana, energy, hunger etc.
    }

    [System.Serializable]
    class ProgressionStat
    {   public Stat stat;
        public float[] levels;
        //you can add anything that you wnat to improve by level up. Like mana, energy, hunger etc.
    }

}
}


 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Stats
{
public class BaseStats : MonoBehaviour
{
   [Range(1,99)]
   [SerializeField] int startingLevel =1;
   [SerializeField] CharacterClass characterClass;
   [SerializeField] Progression progression = null;
   [SerializeField] GameObject levelUpParticleEffect=null;

    public event Action onLevelUp;

   int currentLevel = 0;
   private void Start()
   {
    currentLevel=CalculateLevel();
    Experience experience =GetComponent<Experience>();
    if(experience != null)
    {
        experience.onExperienceGained += UpdateLevel;//this is not a function call. We are addinh this function to the list
    }

   }
   
    public void UpdateLevel()
    {        
        int newLevel =CalculateLevel();
        if(newLevel>currentLevel)
        {
            currentLevel=newLevel;
           LevelUpEffect();
           onLevelUp();
        }
    }
    private void LevelUpEffect()
    {
        Instantiate(levelUpParticleEffect,transform);
    }
    
         public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetXP();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel+1;
        }
      
}
}


using UnityEngine;
using GameDevTV.Utils;
using RPG.Stats;
namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 200;
        [SerializeField] float manaRegenRate=2;

          LazyValue<float> mana;

        private void Awake() {
           
              mana = new LazyValue<float>(GetMaxMana);
        }
        private void Update() {
         if (mana.value < GetMaxMana())
         {
            mana.value += GetRegenRate() * Time.deltaTime;
        if (mana.value > GetMaxMana())
          {
          mana.value = GetMaxMana();
          }}
        }

        public float GetMana()
        {
           return mana.value;
        }

        public float GetMaxMana()
        {
           return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
             if (manaToUse > mana.value)
            {
                return false;
            }
         mana.value -= manaToUse;
            return true;
        }
    }
}
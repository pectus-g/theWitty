using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;
    // Start is called before the first frame update
    public void GiveQuest()
    {
QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
questList.AddQuest(quest);
    }
}

}

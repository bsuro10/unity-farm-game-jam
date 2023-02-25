using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(menuName = "Quests/New Quest", fileName = "New Quest")]
    public class QuestData : ScriptableObjectWithIdAttribute
    {
        [Serializable]
        public struct Info
        {
            public string name;
            public string description;
        }

        [Header("Info")]
        public Info information;

        [Serializable]
        public struct Reward
        {
            public int currency;
            public List<Item> items;
        }

        [Header("Reward")]
        public Reward reward;

        [Header("Prerequisites")]
        public List<QuestData> prerequisitesQuests;

        [Header("Goals")]
        [SerializeReference] public List<QuestGoal> goals;

        public void AddGoal(QuestGoal questGoal)
        {
            goals.Add(questGoal);
        }
    }

}

using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Npc", menuName = "Npc/New Npc")]
    public class NpcData : ScriptableObject
    {
        new public string name;
        public Color nameColor = Color.black;

    }
}
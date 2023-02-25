using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue")]
    public class DialogueData : ScriptableObject
    {

        [System.Serializable]
        public struct DialogueSentence
        {
            public NpcData npcData;
            [TextArea(4, 10)] public string sentence;
        }

        public DialogueSentence[] sentences;

    }
}

using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Crop", menuName = "Crop/New Crop")]
    public class Crop : ScriptableObject
    {
        public struct CropStage
        {
            public Sprite sprite;
            public int growthTime;
        }

        public int timeToGrow = 10;
        public Item yield;
        public int count = 1;
        public CropStage[] stages;
    }
}

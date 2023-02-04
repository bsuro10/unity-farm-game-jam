using UnityEngine;
using UnityEngine.Tilemaps;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "New Crop", menuName = "Crop/New Crop")]
    public class Crop : ScriptableObject
    {
        [System.Serializable]
        public struct CropStage
        {
            public Tile tile;
            public int growthTime;
        }

        public int timeToGrow = 10;
        public Item yield;
        public int yieldAmount = 1;
        public CropStage[] stages;
    }
}

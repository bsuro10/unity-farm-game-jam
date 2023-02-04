using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FarmGame
{
    public class CropTile { }

    public class CropsManager : MonoBehaviour
    {
        #region Singleton
        public static CropsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField] private Tilemap seedsTilemap;
        [SerializeField] private Tile plowTile;

        private TileManager tileManager;
        private Dictionary<Vector3Int, CropTile> crops;
        private Dictionary<Vector3Int, Seed> seeds;

        private void Start()
        {
            tileManager = TileManager.Instance;
            crops = new Dictionary<Vector3Int, CropTile>();
            seeds = new Dictionary<Vector3Int, Seed>();
        }

        public void PlowGround(Vector3 position)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if ((!crops.ContainsKey(cellPositionForTile)) &&
                (tileManager.IsInteractable(cellPositionForTile)))
            {
                crops.Add(cellPositionForTile, new CropTile());
                tileManager.SetCustomTile(cellPositionForTile, plowTile, tileManager.interactableMap);
            }
        }

        public bool Seed(Vector3 position, Crop cropToSeed)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if (IsCropExist(cellPositionForTile) && !IsSeedExist(cellPositionForTile))
            {
                /*seeds.Add(cellPositionForTile, seed);
                tileManager.SetCustomTile(position, seed.seedTile, seedsTilemap);*/
                return true;
            }
            return false;
        }

        private bool IsCropExist(Vector3Int position)
        {
            return crops.ContainsKey(position);
        }

        private bool IsSeedExist(Vector3Int position)
        {
            return seeds.ContainsKey(position);
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FarmGame
{
    public class CropTile 
    {
        public int growTimer;
        public int growStage;
        public Crop crop;
        public bool isGrowthCompleted
        { 
            get
            {
                return (crop != null) && (growTimer >= crop.timeToGrow);
            }
        }

        public void Harvested()
        {
            growTimer = 0;
            growStage = 0;
            crop = null;
        }

    }

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

        private void Start()
        {
            TimeManager.Instance.onTimeTick += OnTimeTick;
            tileManager = TileManager.Instance;
            crops = new Dictionary<Vector3Int, CropTile>();
        }

        public void OnTimeTick()
        {
            foreach (KeyValuePair<Vector3Int, CropTile> crop in crops)
            {
                Vector3Int cellPosition = crop.Key;
                CropTile cropTile = crop.Value;
                if ((cropTile.crop != null) && (!cropTile.isGrowthCompleted))
                {
                    cropTile.growTimer++;

                    if (cropTile.growTimer >= cropTile.crop.stages[cropTile.growStage].growthTime)
                    {
                        tileManager.SetCustomTile(cellPosition, cropTile.crop.stages[cropTile.growStage].tile, seedsTilemap);
                        cropTile.growStage++;
                    }
                }
            }
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
            if (CanSeedBePlanted(cellPositionForTile, cropToSeed))
            {
                tileManager.SetCustomTile(position, cropToSeed.stages[0].tile, seedsTilemap);
                crops[cellPositionForTile].crop = cropToSeed;
                return true;
            }
            return false;
        }

        public void Harvest(Vector3 position)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if (crops.ContainsKey(cellPositionForTile))
            {
                CropTile cropTile = crops[cellPositionForTile];
                if (cropTile.isGrowthCompleted)
                {
                    ItemSpawnManager.Instance.SpawnItem(
                        seedsTilemap.GetCellCenterWorld(cellPositionForTile),
                        cropTile.crop.yield,
                        cropTile.crop.yieldAmount
                    );
                    tileManager.ClearTile(cellPositionForTile, seedsTilemap);
                    tileManager.SetCustomTile(cellPositionForTile, plowTile, tileManager.interactableMap);
                    cropTile.Harvested();
                }
            }
        }

        private bool CanSeedBePlanted(Vector3Int position, Crop cropToSeed)
        {
            return crops.ContainsKey(position) && 
                crops[position].crop == null &&
                cropToSeed.stages.Length > 0;
        }
        
    }
}
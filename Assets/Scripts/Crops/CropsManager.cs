using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FarmGame
{
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
        [SerializeField] private AudioClip plowGroundSound;
        [SerializeField] private AudioClip seedGroundSound;
        [SerializeField] private AudioClip harvestGroundSound;

        private TileManager tileManager;
        private CropsDataManager cropsDataManager;
        private SoundManager soundManager;

        private void Start()
        {
            tileManager = TileManager.Instance;
            cropsDataManager = CropsDataManager.Instance;
            soundManager = SoundManager.Instance;
            cropsDataManager.onCropStageChanged += OnCropStageChanged;
            InitializeCropsFromDataManager();
        }

        private void OnDestroy()
        {
            cropsDataManager.onCropStageChanged -= OnCropStageChanged;
        }

        public void PlowGround(Vector3 position)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if ((!cropsDataManager.crops.ContainsKey(cellPositionForTile)) &&
                (tileManager.IsInteractable(cellPositionForTile)))
            {
                if (plowGroundSound != null)
                    soundManager.PlaySound(plowGroundSound);
                cropsDataManager.crops.Add(cellPositionForTile, new CropTile());
                tileManager.SetCustomTile(cellPositionForTile, plowTile, tileManager.interactableMap);
            }
        }

        public bool Seed(Vector3 position, Crop cropToSeed)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if (CanSeedBePlanted(cellPositionForTile, cropToSeed))
            {
                if (seedGroundSound != null)
                    soundManager.PlaySound(seedGroundSound);
                tileManager.SetCustomTile(position, cropToSeed.stages[0].tile, seedsTilemap);
                cropsDataManager.crops[cellPositionForTile].crop = cropToSeed;
                return true;
            }
            return false;
        }

        public void Harvest(Vector3 position)
        {
            Vector3Int cellPositionForTile = tileManager.gridLayout.WorldToCell(position);
            if (cropsDataManager.crops.ContainsKey(cellPositionForTile))
            {
                CropTile cropTile = cropsDataManager.crops[cellPositionForTile];
                if (cropTile.isGrowthCompleted)
                {
                    if (harvestGroundSound != null)
                        soundManager.PlaySound(harvestGroundSound);
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

        private void OnCropStageChanged(KeyValuePair<Vector3Int, CropTile> cropTileKeyPair)
        {
            Vector3Int cellPosition = cropTileKeyPair.Key;
            CropTile cropTile = cropTileKeyPair.Value;
            tileManager.SetCustomTile(cellPosition, cropTile.crop.stages[cropTile.growStage].tile, seedsTilemap);
        }

        private bool CanSeedBePlanted(Vector3Int position, Crop cropToSeed)
        {
            return cropsDataManager.crops.ContainsKey(position) &&
                cropsDataManager.crops[position].crop == null &&
                cropToSeed.stages.Length > 0;
        }

        private void InitializeCropsFromDataManager()
        {
            foreach (KeyValuePair<Vector3Int, CropTile> crop in cropsDataManager.crops)
            {
                Vector3Int cellPosition = crop.Key;
                CropTile cropTile = crop.Value;
                tileManager.SetCustomTile(cellPosition, plowTile, tileManager.interactableMap);

                if (cropTile.crop != null)
                    tileManager.SetCustomTile(cellPosition, cropTile.crop.stages[cropTile.growStage].tile, seedsTilemap);
            }
        }


    }
}
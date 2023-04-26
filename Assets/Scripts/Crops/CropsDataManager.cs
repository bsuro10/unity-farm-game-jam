using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class CropsDataManager : MonoBehaviour
    {
        #region Singleton
        public static CropsDataManager Instance { get; private set; }

        private void Awake()
        {
            crops = new Dictionary<Vector3Int, CropTile>();
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

        public delegate void OnCropStageChanged(KeyValuePair<Vector3Int, CropTile> crop);
        public OnCropStageChanged onCropStageChanged;

        public Dictionary<Vector3Int, CropTile> crops { get; private set; }

        private void Start()
        {
            TimeManager.Instance.onTimeTick += OnTimeTick;
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
                        if (onCropStageChanged != null)
                            onCropStageChanged.Invoke(crop);

                        cropTile.growStage++;
                    }
                }
            }
        }
    }
}

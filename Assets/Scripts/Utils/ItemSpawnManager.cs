using UnityEngine;

namespace FarmGame
{
    public class ItemSpawnManager : MonoBehaviour
    {
        #region Singleton
        public static ItemSpawnManager Instance { get; private set; }

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

        [SerializeField] GameObject collectibleItemPrefab;

        public void SpawnItem(Vector3 position, Item item, int amount)
        {
            GameObject spawnedItem = Instantiate(collectibleItemPrefab, position, Quaternion.identity);
            spawnedItem.GetComponent<Collectible>().SetCollectibeItem(item, amount);
        }
    }
}

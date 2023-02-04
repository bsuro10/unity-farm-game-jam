using UnityEngine;
using UnityEngine.Tilemaps;

namespace FarmGame
{
    public class TileManager : MonoBehaviour
    {
        #region Singleton
        public static TileManager Instance { get; private set; }

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

        [SerializeField] public Tilemap interactableMap;
        [SerializeField] private Tile hiddenInteractableTile;
        [SerializeField] private Tile visibleInteractableTile;

        public GridLayout gridLayout { get; private set; }

        private void Start()
        {
            gridLayout = interactableMap.layoutGrid;
            foreach (Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null && tile.name == visibleInteractableTile.name)
                    interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }

        public bool IsInteractable(Vector3Int position)
        {
            TileBase tile = interactableMap.GetTile(position);
            return (tile != null) && (tile.name == hiddenInteractableTile.name);
        }

        public bool IsInteractable(Vector3 position)
        {
            return IsInteractable(gridLayout.WorldToCell(position));
        }

        public void SetCustomTile(Vector3Int position, Tile tile, Tilemap tilemap)
        {
            tilemap.SetTile(position, tile);
        }

        public void SetCustomTile(Vector3 position, Tile tile, Tilemap tilemap)
        {
            SetCustomTile(gridLayout.WorldToCell(position), tile, tilemap);
        }
    }
}

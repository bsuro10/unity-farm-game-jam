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

        [SerializeField] private Tilemap interactableMap;
        [SerializeField] private Tile hiddenInteractableTile;
        [SerializeField] private Tile visibleInteractableTile;

        private GridLayout gridLayout;

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

        public bool IsInteractable(Vector3 position)
        {
            TileBase tile = interactableMap.GetTile(gridLayout.WorldToCell(position));
            return (tile != null) && (tile.name == hiddenInteractableTile.name);
        }

        public void SetCustomTile(Vector3 position, Tile tile)
        {
            interactableMap.SetTile(gridLayout.WorldToCell(position), tile);
        }
    }
}

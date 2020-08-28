using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Tiles
{
    class TileManager : MonoBehaviour
    {

        public static TileManager Instance { get; set; }

        [SerializeField] private GameObject tilePrefab = null;

        [SerializeField] private int width = 10;
        [SerializeField] private int height = 20;

        private Vector2 offset;
        private bool[,] state;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            state = new bool[width, height];

            // Get bottom left position
            float x = width / 2f - 0.5f; // +0.5 to center tiles
            float y = height / 2f - 0.5f;

            offset = new Vector2(x, y);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Create tiles
                    Instantiate(tilePrefab, new Vector2(i, j) - offset, Quaternion.identity, transform);
                }
            }    
        }


        public bool IsPositionFull(Vector2Int position)
        {
            if (position.y < 0)
            {
                // Bottom is always full
                return true;
            }
            else if (position.y >= height)
            {
                // Above is always open
                return false;
            }

            return state[position.x, position.y];
        }

        public Vector2Int ToIndex(Vector2 position)
        {
            // Apply offset
            return Vector2Int.FloorToInt(position + offset);
        }

    }
}

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

        private bool[,] state;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            state = new bool[width, height];

            // Get bottom left position
            float x = -width / 2f + 0.5f; // +0.5 to center tiles
            float y = -height / 2f + 0.5f;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Create tiles
                    Instantiate(tilePrefab, new Vector2(x + i, y + j), Quaternion.identity, transform);
                }
            }    
        }


        public bool IsOpen(int i, int j)
        {
            return state[i, j];
        }

    }
}

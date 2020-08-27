using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {

        [SerializeField] private GameObject tilePrefab = null;
        [SerializeField] private Transform tileParent = null;

        [SerializeField] private int width = 10;
        [SerializeField] private int height = 20;



        private void Start()
        {
            // Get bottom left position
            float x = -width / 2f + 0.5f; // +0.5 to center tiles
            float y = -height / 2f + 0.5f;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Create tiles
                    Instantiate(tilePrefab, new Vector2(x + i, y + j), Quaternion.identity, tileParent);
                }
            }    
        }

    }
}

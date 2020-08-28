﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Tiles
{
    class TileManager : MonoBehaviour
    {

        public enum Side
        {
            LEFT,
            RIGHT,
            NONE,
        }

        public static TileManager Instance { get; set; }


        [SerializeField] private GameObject tilePrefab = null;
        [SerializeField] private Transform tileParent = null;

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
                    Instantiate(tilePrefab, new Vector2(i, j) - offset, Quaternion.identity, tileParent);
                }
            }
        }


        public bool IsPositionFull(Vector2Int position)
        {
            if (position.y < 0 || position.x < 0 || position.x >= width)
            {
                // Bottom and sides are always full
                return true;
            }
            else if (position.y >= height)
            {
                // Above is always open
                return false;
            }

            return state[position.x, position.y];
        }

        public Side IsCollidingWithSide(Vector2Int position)
        {
            if (position.x < 0)
            {
                return Side.LEFT;
            }
            else if (position.x >= width)
            {
                return Side.RIGHT;
            }
            else
            {
                return Side.NONE;
            }
        }


        public void SetPositionFull(Vector2Int position)
        {
            if (position.y < height)
            {
                state[position.x, position.y] = true;
            }
        }


        public Vector2Int ToIndex(Vector2 position)
        {
            // Apply offset
            return Vector2Int.RoundToInt(position + offset);
        }

    }
}

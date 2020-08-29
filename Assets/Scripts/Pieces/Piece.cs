using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.Pieces
{
    partial class Piece : MonoBehaviour
    {

        [SerializeField] private int points = 4;

        private float nextTime;


        private void Start()
        {
            SetTimer();

            if (IsColliding(Vector2Int.down))
            {
                // If piece is immediately invalid, game is over
                GameManager.Instance.EndGame();
            }
        }

        private void Update()
        {
            // Run every interval
            if (Time.time > nextTime)
            {
                SetTimer();

                if (IsColliding(Vector2Int.down))
                {
                    // Stop if colliding
                    Stop();
                }
                else
                {
                    // Move down
                    Move(Vector2.down);
                }
            }
        }


        private bool IsColliding(Vector2Int offset)
        {
            // Test if any piece cannot go down
            foreach (Transform child in transform)
            {
                // Get block position
                Block block = child.GetComponent<Block>();
                Vector2Int position = block.Position + offset;

                // If position below any block is full
                if (TileManager.Instance.IsPositionFull(position))
                {
                    // Stop
                    return true;
                }
            }

            return false;
        }

        private void Stop()
        {
            TileManager tileManager = TileManager.Instance;
            Transform[] children = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                // Get block
                Transform child = transform.GetChild(i);
                children[i] = child;

                // Set position to full
                Block block = child.GetComponent<Block>();
                tileManager.SetPositionFull(block);
            }

            // Transfer blocks to state
            foreach (Transform child in children)
            {
                child.SetParent(tileManager.transform);
            }

            // Remove any rows created
            tileManager.RemoveRows(points);

            // Create new piece
            PieceManager.Instance.CreatePiece();
            Destroy(gameObject);
        }


        public void ResetTimer()
        {
            nextTime = 0;
        }

        private void SetTimer()
        {
            nextTime = Time.time + GameManager.Instance.Speed;
        }

    }
}
using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.Pieces
{
    class Piece : MonoBehaviour
    {

        [SerializeField] private int points = 4;


        private IEnumerator Start()
        {
            if (IsColliding(Vector2Int.down))
            {
                // If piece is immediately invalid, game is over
                GameManager.Instance.EndGame();
                yield break;
            }

            while (true)
            {
                // Wait delay
                yield return new WaitForSeconds(GameManager.Instance.Speed);

                if (IsColliding(Vector2Int.down))
                {
                    Stop();
                    break;
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


        public void Translate(Vector2Int translation)
        {
            // If translation causes collision
            if (IsColliding(translation))
            {
                return;
            }

            Move(translation);
        }

        private void Move(Vector2 translation)
        {
            // Translate relative to the world
            transform.Translate(translation, Space.World);
        }


        public void Rotate(float angle)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            transform.Rotate(Vector3.forward, angle);
            UnhookFromSide();

            // Undo rotation if it causes collision
            if (IsColliding(Vector2Int.zero))
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }

        private void UnhookFromSide()
        {
            while (true)
            {
                foreach (Transform child in transform)
                {
                    // Get block position
                    Block block = child.GetComponent<Block>();
                    Vector2Int position = block.Position;

                    // If any is colliding with side
                    switch (TileManager.Instance.IsCollidingWithSide(position))
                    {
                        case TileManager.Side.LEFT:
                        {
                            Move(Vector2.right);
                            goto Continue;
                        }

                        case TileManager.Side.RIGHT:
                        {
                            Move(Vector2.left);
                            goto Continue;
                        }
                    }
                }

                return;
            Continue:;
            }
        }


        public void Fall()
        {
            // Move down until it cannot
            while (!IsColliding(Vector2Int.down))
            {
                Move(Vector2.down);
            }
        }

    }
}
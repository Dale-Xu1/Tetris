using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.Pieces
{
    partial class Piece : MonoBehaviour
    {

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

            ResetTimer();
        }

    }
}
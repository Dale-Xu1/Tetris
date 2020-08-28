using UnityEngine;
using System.Collections;
using Assets.Scripts.Blocks;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.Pieces
{
    class Piece : MonoBehaviour
    {

        private IEnumerator Start()
        {
            while (true)
            {
                bool stop = TestStop();

                // Wait delay
                yield return new WaitForSeconds(GameManager.Instance.Speed);

                if (stop)
                {
                    Stop();
                    break;
                }
                else
                {
                    // Move down
                    transform.Translate(Vector2.down);
                }
            }
        }


        private bool TestStop()
        {
            // Test if any piece cannot go down
            foreach (Transform child in transform)
            {
                // Get block position
                Block block = child.GetComponent<Block>();
                Vector2Int position = block.Position + Vector2Int.down;

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
            // Transfer blocks to state
            foreach (Transform child in transform)
            {
                // TODO
            }

            // Create new piece
            PieceManager.Instance.CreatePiece();
            Destroy(gameObject);
        }

    }
}
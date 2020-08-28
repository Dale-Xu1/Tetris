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
            if (TestStop())
            {
                // If piece is immediately invalid, game is over
                Debug.Log("Game Over");
                Time.timeScale = 0;

                yield break;
            }

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
            Transform[] children = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                // Get block
                Transform child = transform.GetChild(i);
                children[i] = child;

                // Set position to full
                Block block = child.GetComponent<Block>();
                TileManager.Instance.SetPositionFull(block.Position);
            }

            // Transfer blocks to state
            foreach (Transform child in children)
            {
                child.SetParent(TileManager.Instance.transform);
            }

            // Create new piece
            PieceManager.Instance.CreatePiece();
            Destroy(gameObject);
        }

    }
}
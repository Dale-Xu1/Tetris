using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Pieces
{
    class Piece : MonoBehaviour
    {

        private IEnumerator Start()
        {
            while (true)
            {
                TestStop();

                // Wait delay
                yield return new WaitForSeconds(GameManager.Instance.Speed);
                transform.Translate(Vector2.down); // Move down
            }
        }


        private void TestStop()
        {
            // Test if any piece cannot go down
            foreach (Transform child in transform)
            {
                Piece piece = child.GetComponent<Piece>();
            }
        }

    }
}
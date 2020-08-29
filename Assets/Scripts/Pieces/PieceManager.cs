using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Pieces
{
    class PieceManager : MonoBehaviour
    {

        public static PieceManager Instance { get; private set; }


        [SerializeField] private Piece[] pieces = null;

        private int index = 0;
        private Piece currentPiece;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ShufflePieces();
            CreatePiece();
        }

        private void Update()
        {
            // Translate piece
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentPiece.Translate(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currentPiece.Translate(Vector2Int.right);
            }

            // Rotate piece
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentPiece.Rotate(90);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentPiece.Rotate(-90);
            }

            // Fall piece
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPiece.Fall();
            }

            // Reset timer
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentPiece.ResetTimer();
            }
        }


        public void CreatePiece()
        {
            // Selects and instantiates piece
            Piece piece = pieces[index];

            // If next index is out of bounds
            index++;
            if (index >= pieces.Length)
            {
                index = 0;
                ShufflePieces();
            }

            currentPiece = Instantiate(piece, transform);
        }

        private void ShufflePieces()
        {
            // Fisher yates shuffle
            for (int i = 0; i < pieces.Length - 1; i++)
            {
                int j = Random.Range(i + 1, pieces.Length);

                Piece temp = pieces[i];
                pieces[i] = pieces[j];
                pieces[j] = temp;
            }
        }

    }
}
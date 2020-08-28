using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Pieces
{
    class PieceManager : MonoBehaviour
    {

        public static PieceManager Instance { get; private set; }


        [SerializeField] private Piece[] pieces = null;

        private Piece currentPiece;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
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
        }


        public void CreatePiece()
        {
            // Selects and instantiates random piece
            Piece piece = pieces[Random.Range(0, pieces.Length)];

            currentPiece = Instantiate(piece, transform);
        }

    }
}
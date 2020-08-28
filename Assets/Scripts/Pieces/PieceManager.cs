using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Pieces
{
    class PieceManager : MonoBehaviour
    {

        public static PieceManager Instance { get; set; }


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
        }


        public void CreatePiece()
        {
            // Selects and instantiates random piece
            Piece piece = pieces[Random.Range(0, pieces.Length)];

            currentPiece = Instantiate(piece, transform);
        }

    }
}
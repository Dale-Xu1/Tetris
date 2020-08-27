using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Pieces
{
    class PieceManager : MonoBehaviour
    {

        public static PieceManager Instance { get; set; }


        [SerializeField] private Piece[] pieces = null;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CreatePiece();
        }


        public void CreatePiece()
        {
            // Selects and instantiates random piece
            Piece piece = pieces[Random.Range(0, pieces.Length)];
            Instantiate(piece, transform);
        }

    }
}
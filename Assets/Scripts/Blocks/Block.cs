using UnityEngine;
using System.Collections;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.Blocks
{
    class Block : MonoBehaviour
    {

        public Vector2Int Position => TileManager.Instance.ToIndex(transform.position);

    }
}
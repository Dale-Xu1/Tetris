using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; set; }


        [SerializeField] private float speed = 1f;

        public float Speed => speed;


        private void Awake()
        {
            Instance = this;
        }

    }
}
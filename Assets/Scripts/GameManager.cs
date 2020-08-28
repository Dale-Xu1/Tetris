using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }


        [SerializeField] private float speed = 1f;

        private int score = 0;

        public delegate void ScoreHandler(int score);
        public event ScoreHandler OnScoreUpdate;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            OnScoreUpdate(score);
        }


        public float Speed => speed;

        public void AddPoints(int points)
        {
            // Add points and broadcast event
            score += points;
            speed = 1f - (score * 0.002f);

            OnScoreUpdate(score);
        }

    }
}
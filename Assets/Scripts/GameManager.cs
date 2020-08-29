using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }


        [SerializeField] private float initialSpeed = 1f;
        [SerializeField] private float fastFactor = 0.1f;

        private int score = 0;

        private float speed;
        private bool isFast;

        public delegate void ScoreHandler(int score);
        public event ScoreHandler OnScoreUpdate;

        public delegate void GameOverHandler();
        public event GameOverHandler OnGameOver;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            speed = initialSpeed;
            OnScoreUpdate(score);
        }

        private void Update()
        {
            isFast = Input.GetKey(KeyCode.S);
        }


        public float Speed => isFast ? speed * fastFactor : speed;

        public void AddPoints(int points)
        {
            // Add points and broadcast event
            score += points;
            speed = initialSpeed / (score * 0.001f + 1f);

            OnScoreUpdate(score);
        }


        public void EndGame()
        {
            OnGameOver();
            Time.timeScale = 0; // Stop game from running
        }

    }
}
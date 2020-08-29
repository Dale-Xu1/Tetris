using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }


        [SerializeField] private float speed = 1f;
        [SerializeField] private float fastFactor = 0.1f;

        private int score = 0;
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
            OnScoreUpdate(score);
        }

        private void Update()
        {
            isFast = Input.GetKey(KeyCode.S);
        }


        public float Speed
        {
            get
            {
                if (isFast)
                {
                    // Apply factor is enabled
                    return speed * fastFactor;
                }
                else
                {
                    return speed;
                }
            }
        }

        public void AddPoints(int points)
        {
            // Add points and broadcast event
            score += points;
            speed = 1f - (score * 0.002f);

            OnScoreUpdate(score);
        }


        public void EndGame()
        {
            OnGameOver();
            Time.timeScale = 0; // Stop game from running
        }

    }
}
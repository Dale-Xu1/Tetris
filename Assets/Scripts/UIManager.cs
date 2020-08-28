using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class UIManager : MonoBehaviour
    {

        public static UIManager Instance { get; private set; }


        [SerializeField] private Text scoreText = null;

        [SerializeField] private GameObject gameOver = null;
        [SerializeField] private Text finalScoreText = null;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Listen for when data changes
            GameManager gameManager = GameManager.Instance;

            gameManager.OnScoreUpdate += OnScoreUpdate;
            gameManager.OnGameOver += OnGameOver;
        }


        private void OnScoreUpdate(int score)
        {
            scoreText.text = score.ToString();
        }

        private void OnGameOver()
        {
            // Show game over screen
            finalScoreText.text = scoreText.text;
            gameOver.SetActive(true);
        }

    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


        public void PlayAgain()
        {
            // Reload current scene
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
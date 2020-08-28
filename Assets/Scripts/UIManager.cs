using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class UIManager : MonoBehaviour
    {

        public static UIManager Instance { get; private set; }


        [SerializeField] private Text scoreText = null;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Listen for when score changes
            GameManager.Instance.OnScoreUpdate += OnScoreUpdate;
        }


        private void OnScoreUpdate(int score)
        {
            scoreText.text = score.ToString();
        }

    }
}
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems
{
    public class GameLoopManager : MonoBehaviour
    {
        public static GameLoopManager Instance;

        [SerializeField] private GameObject timerUI;
        [SerializeField] private GameObject scoreUI;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;
        
        private TextMeshProUGUI _timerText;
        private TextMeshProUGUI _scoreText;
        


        private float _timer = 60f;
        private int _score = 0;
        private bool _gameOver = false;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _timerText = timerUI.GetComponentInChildren<TextMeshProUGUI>();   
            _scoreText = scoreUI.GetComponentInChildren<TextMeshProUGUI>();   
            gameOverPanel.SetActive(false);
            
            restartButton.onClick.AddListener(RestartGame);
            StartCoroutine(TimerCountdown());
            
        }

        IEnumerator TimerCountdown()
        {
            while (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _timerText.text = Mathf.CeilToInt(_timer).ToString();
                yield return null;
            }
            EndGame();
        }

        void EndGame()
        {
            _gameOver = true;
            gameOverPanel.SetActive(true);
            gameOverText.text = $"Time is Up! Your score is {_score}";
        }

        public void AddScore()
        {
            if (!_gameOver)
            {
                _score++;
                _scoreText.text = _score.ToString();
            }
        }

        void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
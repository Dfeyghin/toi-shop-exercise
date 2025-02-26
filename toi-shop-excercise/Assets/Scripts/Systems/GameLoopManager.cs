using System.Collections;
using System.Collections.Generic;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems
{
    public class GameLoopManager : MonoBehaviour
    {
        public static GameLoopManager Instance;
        [SerializeField] public Constants constants;

        [SerializeField] private GameObject timerUI;
        [SerializeField] private GameObject scoreUI;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;
        [SerializeField] private List<GameObject> characters;        
        [SerializeField] private GameObject selectionIndicatorPrefab;


        private TextMeshProUGUI _timerText;
        private TextMeshProUGUI _scoreText;
        
        private float _timer;
        private int _score = 0;
        public bool isGameOver = false;
        private GameObject _selectionIndicator;
        private Character _selectedCharacter = null;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            SpawnCharacters();
            _timer = constants.Timing.gameDuration;
            _timerText = timerUI.GetComponentInChildren<TextMeshProUGUI>();   
            _scoreText = scoreUI.GetComponentInChildren<TextMeshProUGUI>();   
            gameOverPanel.SetActive(false);
            
            _selectionIndicator = Instantiate(selectionIndicatorPrefab);
            _selectionIndicator.SetActive(false);
            
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
            isGameOver = true;
            gameOverPanel.SetActive(true);
            gameOverText.text = $"Time is Up! Your score is {_score}";
        }

        public void AddScore()
        {
            if (!isGameOver)
            {
                _score++;
                _scoreText.text = _score.ToString();
            }
        }

        void SpawnCharacters()
        {
            List<Region> availableRegions = RegionsController.Instance.Regions.FindAll(r => !r.IsOccupied);;
            for (int i = 0; i < characters.Count; i++)
            {
                if (availableRegions.Count == 0 || availableRegions.Count < characters.Count)
                {
                    Debug.LogWarning("Not enough available regions to place all characters!");
                    return;
                }
                
                int randomIndex = Random.Range(0, availableRegions.Count);
                Region chosenRegion = availableRegions[randomIndex];
                
                GameObject characterInstance = Instantiate(characters[i], chosenRegion.transform.position, Quaternion.identity);
                Character characterComponent = characterInstance.GetComponent<Character>();
                
                characterComponent.CurrentRegion = chosenRegion;
                chosenRegion.OccupyingCharacter = characterComponent;
                
                chosenRegion.IsOccupied = true;
                
                availableRegions.RemoveAt(randomIndex);
            }
        }
        
        private void UpdateSelectionIndicator()
        {
            if (_selectedCharacter != null)
            {
                _selectionIndicator.transform.position = _selectedCharacter.transform.position + Vector3.up;
                _selectionIndicator.SetActive(true);
                _selectionIndicator.transform.SetParent(_selectedCharacter.transform);
            }
            else
            {
                _selectionIndicator.SetActive(false);
                _selectionIndicator.transform.SetParent(null);
            }
        }
        
        private void SelectCharacter(Character character)
        {
            _selectedCharacter = character;
            UpdateSelectionIndicator();
        }

        public void OnRegionClicked(Region region)
        {
            if (isGameOver) return;

            if (region.IsOccupied)
            {
                SelectCharacter(region.OccupyingCharacter);
            }
            else if (_selectedCharacter != null && _selectedCharacter.CurrentRegion.IsAdjacentTo(region))
            {
                _selectedCharacter.MoveToRegion(region);
            }
        }

        void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}

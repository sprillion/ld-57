using System;
using System.Collections.Generic;
using System.Linq;
using items;
using UnityEngine;
using UnityEngine.UI;

namespace level
{
    public class LevelService : MonoBehaviour
    {
        public static LevelService Instance;

        [SerializeField] private Button _nextLevelButton;
        
            
        private List<LevelData> _levelDatas;
        private int _currentLevelNumber;
        public Level CurrentLevel { get; private set; }

        public event Action OnLevelComplete;

        public void Initialize()
        {
            Instance = this;
            _levelDatas = Resources.LoadAll<LevelData>("Level/").ToList();
        }

        private void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
            LoadLevel();
        }
        
        private void Update()
        {
            if (!_nextLevelButton.gameObject.activeSelf) return;
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                _nextLevelButton.onClick?.Invoke();
            }
        }

        public LevelData GetCurrentData()
        {
            return _levelDatas[_currentLevelNumber];
        }

        public void ReadyToNext()
        {
            _nextLevelButton.gameObject.SetActive(true);
        }

        private void LoadLevel()
        {
            if (CurrentLevel)
            {
                Destroy(CurrentLevel.gameObject);
                CurrentLevel = null;
            }

            var nextLevel = _currentLevelNumber % (_levelDatas.Count - 1);
            CurrentLevel = Instantiate(Resources.Load<Level>($"Prefabs/Levels/Level_{nextLevel}"));
        }

        private void NextLevel()
        {
            _nextLevelButton.gameObject.SetActive(false);
            _currentLevelNumber++;
            ItemService.Instance.Clear();
            
            LoadLevel();
            OnLevelComplete?.Invoke();
        }
        
        
    }
}
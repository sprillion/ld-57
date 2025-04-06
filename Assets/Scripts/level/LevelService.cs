using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using items;
using UnityEngine;
using UnityEngine.UI;

namespace level
{
    public class LevelService : MonoBehaviour
    {
        public static LevelService Instance;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Image _fill;
            
        private List<LevelData> _levelDatas;

        private Sequence _sequence;
        
        public int CurrentLevelNumber { get; private set; }
        public Level CurrentLevel { get; private set; }

        public event Action OnLevelStart;
        public event Action OnLevelComplete;

        public void Initialize()
        {
            Instance = this;
            _levelDatas = Resources.LoadAll<LevelData>("Level/").ToList();
        }

        private void Start()
        {
            _nextLevelButton.onClick.AddListener(LaunchNextLevel);
            LoadLevel();
        }
        
        private void Update()
        {
            if (!_nextLevelButton.gameObject.activeSelf) return;
            if (!Boot.HaveControl) return;
            if (Input.GetKeyDown(KeyCode.G))
            {
                _nextLevelButton.onClick?.Invoke();
            }
        }

        public LevelData GetCurrentData()
        {
            return _levelDatas[CurrentLevelNumber];
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

            var nextLevel = CurrentLevelNumber % (_levelDatas.Count - 1);
            CurrentLevel = Instantiate(Resources.Load<Level>($"Prefabs/Levels/Level_{nextLevel}"));
            OnLevelStart?.Invoke();
        }

        private void LaunchNextLevel()
        {
            OnLevelComplete?.Invoke();
            
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _fill.DOFade(0, 0);
            _fill.gameObject.SetActive(true);
            
            _sequence.Append(_fill.DOFade(1, 2f));
            _sequence.AppendCallback(NextLevel);
            _sequence.Append(_fill.DOFade(0, 2f));
        }

        private void NextLevel()
        {
            _nextLevelButton.gameObject.SetActive(false);
            CurrentLevelNumber++;
            ItemService.Instance.Clear();
            
            LoadLevel();
        }
        
        
    }
}
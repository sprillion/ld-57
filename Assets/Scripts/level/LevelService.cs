using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace level
{
    public class LevelService : MonoBehaviour
    {
        public static LevelService Instance;
        
        private List<LevelData> _levelDatas;
        private int _currentLevel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _levelDatas = Resources.LoadAll<LevelData>("Level/").ToList();
        }

        public LevelData GetCurrentData()
        {
            return _levelDatas[_currentLevel];
        }
    }
}
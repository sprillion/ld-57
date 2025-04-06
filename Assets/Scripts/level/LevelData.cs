using System;
using System.Collections.Generic;
using items;
using UnityEngine;

namespace level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public List<LevelObjective> Objectives = new List<LevelObjective>();
    }

    [Serializable]
    public class LevelObjective
    {
        public ItemType ItemType;
        public int Count;
    }
}
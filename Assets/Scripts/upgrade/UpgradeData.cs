using System.Collections.Generic;
using UnityEngine;

namespace upgrades
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData")]
    public class UpgradeData : ScriptableObject
    {
        public List<float> Values;
        public List<int> Prices;
    }
}
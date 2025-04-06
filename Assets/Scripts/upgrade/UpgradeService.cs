using System;
using System.Collections.Generic;
using items;
using UnityEngine;

namespace upgrades
{
    public class UpgradeService
    {
        public static UpgradeService Instance;

        private readonly UpgradeData _upgradeData;

        private readonly Dictionary<UpgradeType, int> _levels = new Dictionary<UpgradeType, int>();
        
        public UpgradeService()
        {
            Instance = this;
            _upgradeData = Resources.Load<UpgradeData>("UpgradeData");
            
            foreach (UpgradeType value in Enum.GetValues(typeof(UpgradeType)))
            {
                _levels.TryAdd(value, 0);
            }
        }

        public float GetValue(UpgradeType upgradeType)
        {
            return _upgradeData.Values[_levels[upgradeType]];
        }

        public int GetPrice(UpgradeType upgradeType)
        {
            return _upgradeData.Prices[_levels[upgradeType] + 1];
        }

        public void Upgrade(UpgradeType upgradeType)
        {
            if (!CanUpgrade(upgradeType)) return;
            if (!ItemService.Instance.RemoveMoney(GetPrice(upgradeType))) return;
            _levels[upgradeType]++;
        }
        
        public bool CanUpgrade(UpgradeType upgradeType)
        {
            return HaveUpgrade(upgradeType) &&
                   ItemService.Instance.Money >= GetPrice(upgradeType);
        }
        
        public bool HaveUpgrade(UpgradeType upgradeType)
        {
            return _levels[upgradeType] + 1 < _upgradeData.Values.Count;
        }
    }
}
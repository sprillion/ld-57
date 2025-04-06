using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using upgrades;

namespace ui
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private UpgradeType _upgradeType;
        
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;

        private void Awake()
        {
            _buyButton.onClick.AddListener(Upgrade);
        }

        public void UpdateValues()
        {
            if (UpgradeService.Instance.HaveUpgrade(_upgradeType))
            {
                _priceText.text = UpgradeService.Instance.GetPrice(_upgradeType).ToString();
            }
            else
            {
                _priceText.text = "FULL";
            }

            _buyButton.interactable = UpgradeService.Instance.CanUpgrade(_upgradeType);
        }

        private void Upgrade()
        {
            UpgradeService.Instance.Upgrade(_upgradeType);
            UpdateValues();
        }
    }
}
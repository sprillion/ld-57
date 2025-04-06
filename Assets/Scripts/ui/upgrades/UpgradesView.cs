using System;
using System.Collections.Generic;
using items;
using UnityEngine;

namespace ui
{
    public class UpgradesView : MonoBehaviour
    {
        [SerializeField] private List<UpgradeView> _upgrades;
        

        public void Toggle()
        {
            if (!Boot.HaveControl && !gameObject.activeSelf) return;
            
            gameObject.SetActive(!gameObject.activeSelf);
            
            Cursor.lockState = gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            
            Boot.HaveControl = !gameObject.activeSelf;
        }

        private void OnEnable()
        {
            UpdateValues();
            ItemService.Instance.OnMoneyChanged += UpdateValues;
        }

        private void OnDisable()
        {
            ItemService.Instance.OnMoneyChanged -= UpdateValues;
        }

        private void UpdateValues()
        {
            _upgrades.ForEach(upgrade => upgrade.UpdateValues());
        }
    }
}
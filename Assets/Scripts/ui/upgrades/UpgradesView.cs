using System.Collections.Generic;
using character;
using UnityEngine;

namespace ui
{
    public class UpgradesView : MonoBehaviour
    {
        [SerializeField] private List<UpgradeView> _upgrades;
        
        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            
            Cursor.lockState = gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            
            Boot.HaveControl = !gameObject.activeSelf;
        }

        private void OnEnable()
        {
            _upgrades.ForEach(upgrade => upgrade.UpdateValues());
        }
    }
}
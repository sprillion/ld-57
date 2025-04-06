using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UpgradesView _upgradesView;

        private void Awake()
        {
            _button.onClick.AddListener(_upgradesView.Toggle);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _button.onClick?.Invoke();
            }
        }
    }
}
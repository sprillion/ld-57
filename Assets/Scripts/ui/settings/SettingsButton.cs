using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _button.onClick?.Invoke();
            }
        }
    }
}
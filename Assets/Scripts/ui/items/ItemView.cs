using items;
using TMPro;
using UnityEngine;

namespace ui
{
    public class ItemView : MonoBehaviour
    {
        public ItemType ItemType;
        
        [SerializeField] private TMP_Text _countText;

        private int _targetValue;

        public void SetTargetValue(int value)
        {
            _targetValue = value;
        }

        public void SetValue(int value)
        {
            _countText.text = $"{value}/{_targetValue}";
        }
    }
}
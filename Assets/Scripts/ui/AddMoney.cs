using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ui
{
    public class AddMoney : PooledObject
    {
        [SerializeField] private TMP_Text _value;
        private RectTransform _rect => transform as RectTransform;

        public void Show(int value)
        {
            _value.text = $"+{value}";
            gameObject.SetActive(true);
            _rect.anchoredPosition = new Vector2(200, -125);
            _rect.DOAnchorPosY(125, 1f).SetEase(Ease.InOutSine).OnComplete(Release);
        }
        
    }
}
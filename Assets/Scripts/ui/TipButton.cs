using System.Linq;
using DG.Tweening;
using level;
using UnityEngine;
using UnityEngine.UI;
using vfx;

namespace ui
{
    public class TipButton : MonoBehaviour
    {
        private const float Cooldown = 60f;

        [SerializeField] private Button _button;
        [SerializeField] private Image _cooldown;

        private float _timer;

        private void Start()
        {
            _button.onClick.AddListener(Use);
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.F))
            {
                _button.onClick?.Invoke();
            }
        }

        private void Use()
        {
            if (_timer > 0) return;

            var items = LevelService.Instance.CurrentLevel.GetAvailableItems();
            if (items.Count == 0) return;
            _timer = Cooldown;
            
            _cooldown.gameObject.SetActive(true);
            _cooldown.fillAmount = 1;
            _cooldown.DOFillAmount(0, Cooldown).SetEase(Ease.Linear)
                .OnComplete(() => _cooldown.gameObject.SetActive(false));
            
            var item = items.First();

            item.CurrentTip = VfxService.Instance.ShowTip(item.transform.position);
        }
    }
}
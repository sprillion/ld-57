using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UpgradesView _upgradesView;
        
        [Header("Settings")]
        [SerializeField] private float _colorChangeDuration = 2f; // Длительность перехода между цветами

        private Image _targetImage;
        private Sequence _rainbowSequence;

        private readonly Color[] _rainbowColors = new Color[]
        {
            new Color(1f, 0.36f, 0.38f),    // Красный
            new Color(1f, 0.63f, 0.27f),  // Оранжевый
            new Color(1f, 1f, 0.51f),    // Жёлтый
            new Color(0.5f, 1f, 0.51f),    // Зелёный
            new Color(0.56f, 0.69f, 1f),    // Голубой
            new Color(0.85f, 0.67f, 1f), // Индиго
            new Color(1f, 0.77f, 0.81f)  // Фиолетовый
        };

        private void Awake()
        {
            _button.onClick.AddListener(_upgradesView.Toggle);
            _targetImage = GetComponent<Image>();
            CreateRainbowSequence();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _button.onClick?.Invoke();
            }
        }

        private void CreateRainbowSequence()
        {
            _rainbowSequence = DOTween.Sequence();
        
            foreach (var color in _rainbowColors)
            {
                _rainbowSequence.Append(
                    _targetImage.DOColor(color, _colorChangeDuration)
                        .SetEase(Ease.Linear)
                );
            }
            _rainbowSequence.SetLoops(-1, LoopType.Yoyo);
            _rainbowSequence.Play();
        }

        private void OnDestroy()
        {
            _rainbowSequence?.Kill();
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace character
{
    public class Brush : MonoBehaviour
    {
        [SerializeField] private Transform _rotate;
        [SerializeField] private float _angle;
        [SerializeField] private float _interval;

        private Sequence _sequence;

        public void Show(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            _sequence?.Kill();
            _rotate.eulerAngles = Vector3.right * -_angle;
            _sequence = DOTween.Sequence();

            _sequence.Append(_rotate.DOLocalRotate(Vector3.right * _angle, _interval).SetEase(Ease.InOutCubic));
            _sequence.Append(_rotate.DOLocalRotate(Vector3.right * -_angle, _interval).SetEase(Ease.InOutCubic));

            _sequence.SetLoops(3, LoopType.Restart).OnComplete(Hide);
        }

        public void Hide()
        {
            _sequence?.Kill();
            gameObject.SetActive(false);
        }
    }
}
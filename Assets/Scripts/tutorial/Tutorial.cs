using items;
using UnityEngine;
using vfx;

namespace tutorial
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Transform _tipPoint;

        private void Start()
        {
            ItemService.Instance.OnItemsCountChanged += HideTip;
            VfxService.Instance.ShowTip(_tipPoint.position);
        }

        private void OnDestroy()
        {
            ItemService.Instance.OnItemsCountChanged -= HideTip;
        }

        private void HideTip(ItemType itemType)
        {
            if (itemType != ItemType.Bone) return;
            ItemService.Instance.OnItemsCountChanged -= HideTip;
            VfxService.Instance.HideTip();
        }
    }
}
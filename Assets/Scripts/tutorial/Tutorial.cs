using items;
using UnityEngine;
using vfx;

namespace tutorial
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Item _itemToTip;

        private void Start()
        {
            _itemToTip.CurrentTip = VfxService.Instance.ShowTip(_itemToTip.transform.position);
        }
        
    }
}
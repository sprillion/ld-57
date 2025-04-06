using dialog;
using UnityEngine;

namespace items
{
    public class Secret : Item
    {
        [SerializeField] private DialogType _dialogType;
        
        public override void Take()
        {
            DialogService.Instance.PlayDialog(_dialogType);
            base.Take();
        }
    }
}
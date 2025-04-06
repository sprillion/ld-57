using UnityEngine;

namespace items
{
    public class Bone : Item
    {
        [SerializeField] private GameObject _enabledBone;
        [SerializeField] private GameObject _disabledBone;
        
        public override void Take()
        {
            if (Taken) return;
            
            Taken = true;
            _disabledBone.SetActive(false);
            _enabledBone.SetActive(true);
        }
    }
}
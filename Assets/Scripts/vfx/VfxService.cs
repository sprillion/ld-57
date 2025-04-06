using UnityEngine;

namespace vfx
{
    public class VfxService
    {

        public static VfxService Instance;
        
        private readonly Dust _dust;
        private readonly Tip _tip;
        
        public VfxService()
        {
            Instance = this;

            _dust = GameObject.Instantiate(Resources.Load<Dust>("Prefabs/Dust"));
            _tip = GameObject.Instantiate(Resources.Load<Tip>("Prefabs/Tip"));
            _dust.gameObject.SetActive(false);
            _tip.Hide();
        }

        public void PlayDust(Vector3 position)
        {
            _dust.Play(position);
        }

        public void ShowTip(Vector3 position)
        {
            _tip.Show(position);
        }

        public void HideTip()
        {
            _tip.Hide();
        }
    }
}
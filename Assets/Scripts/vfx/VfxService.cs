using UnityEngine;

namespace vfx
{
    public class VfxService
    {
        public static VfxService Instance;
        
        private readonly Dust _dust;

        private readonly ObjectPool _tipPool;
        
        public VfxService()
        {
            Instance = this;

            _tipPool = new ObjectPool(Resources.Load<Tip>("Prefabs/Tip"), 1);
            _dust = GameObject.Instantiate(Resources.Load<Dust>("Prefabs/Dust"));
            _dust.gameObject.SetActive(false);
        }

        public void PlayDust(Vector3 position)  
        {
            _dust.Play(position);
        }

        public Tip ShowTip(Vector3 position)
        {
            var tip = _tipPool.GetObject<Tip>();
            tip.Show(position);
            return tip;
        }
    }
}
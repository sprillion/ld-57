using UnityEngine;

namespace vfx
{
    public class Tip : PooledObject
    {
        public void Show(Vector3 position)
        {
            position.y = 0;
            transform.position = position;
            gameObject.SetActive(true);
        }
    }
}
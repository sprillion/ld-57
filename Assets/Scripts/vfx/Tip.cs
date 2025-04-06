using UnityEngine;

namespace vfx
{
    public class Tip : MonoBehaviour
    {
        public void Show(Vector3 position)
        {
            position.y = 0;
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
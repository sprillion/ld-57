using items;
using sfx;
using UnityEngine;

namespace character
{
    public class ItemTaker : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckItem();
            }
        }

        private void CheckItem()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, LayerMask.NameToLayer("Item"))) return;
            if (Vector3.Distance(_camera.transform.position, hit.point) > _maxDistance) return;
            if (!hit.collider.gameObject.TryGetComponent(out Item item)) return;
            if (!item.CheckFind()) return;
            item.Take();
            ItemService.Instance.AddItem(item);
            SoundService.Instance.PlaySound(SoundType.GetItem);
        }
    }
}
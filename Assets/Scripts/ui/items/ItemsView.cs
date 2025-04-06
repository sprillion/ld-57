using System.Collections.Generic;
using items;
using level;
using TMPro;
using UnityEngine;

namespace ui
{
    public class ItemsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private List<ItemView> _items;

        private readonly Dictionary<ItemType, ItemView> _itemsMap = new Dictionary<ItemType, ItemView>();

        private void Start()
        {
            ItemService.Instance.OnItemsCountChanged += OnItemCountChanged;
            ItemService.Instance.OnMoneyChanged += SetMoney;
            LevelService.Instance.OnLevelStart += LoadData;
            
            _items.ForEach(i => _itemsMap.TryAdd(i.ItemType, i));
            
            LoadData();
        }

        private void LoadData()
        {
            var data = LevelService.Instance.GetCurrentData();
            
            _items.ForEach(i => i.gameObject.SetActive(false));
            
            data.Objectives.ForEach(obj =>
            {
                _itemsMap[obj.ItemType].SetTargetValue(obj.Count);
                _itemsMap[obj.ItemType].gameObject.SetActive(true);
                OnItemCountChanged(obj.ItemType);
            });
        }
        
        private void OnItemCountChanged(ItemType itemType)
        {
            _itemsMap[itemType].SetValue(ItemService.Instance.GetItemCount(itemType));
            SetMoney();
        }

        private void SetMoney()
        {
            _moneyText.text = $"{ItemService.Instance.Money}";
        }
    }
}
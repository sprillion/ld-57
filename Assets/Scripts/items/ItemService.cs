using System;
using System.Collections.Generic;
using System.Linq;
using level;
using ui;
using UnityEngine;

namespace items
{
    public class ItemService
    {
        private readonly Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>();
        
        private int _objectiveCount;

        private readonly ObjectPool _addMoneyPool;
        
        public static ItemService Instance;
        public event Action<ItemType> OnItemsCountChanged;

        public int Money { get; private set; }


        public event Action OnMoneyChanged;

        public ItemService()
        {
            Instance = this;
            LevelService.Instance.OnLevelStart += Clear;
            Clear();

            _addMoneyPool = new ObjectPool(Resources.Load<AddMoney>("Prefabs/AddMoney"), 2, LevelService.Instance.UiParent);
        }

        public void Clear()
        {
            _items.Clear();
            foreach (ItemType value in Enum.GetValues(typeof(ItemType)))
            {
                _items.TryAdd(value, 0);
            }

            _objectiveCount = LevelService.Instance.GetCurrentData().Objectives.First(o => o.ItemType == ItemType.Bone)
                .Count;
        }

        public void AddItem(Item item)
        {
            _items[item.ItemType]++;
            Money += item.Price;
            ShowAddMoney(item.Price);
            OnItemsCountChanged?.Invoke(item.ItemType);
            CheckObjective();
        }

        public int GetItemCount(ItemType itemType)
        {
            return _items[itemType];
        }

        public bool RemoveMoney(int value)
        {
            if (value > Money) return false;
            Money -= value;
            OnMoneyChanged?.Invoke();
            return true;
        }

        private void CheckObjective()
        {
            if (_items[ItemType.Bone] < _objectiveCount) return;
            LevelService.Instance.ReadyToNext();
        }

        private void ShowAddMoney(int value)
        {
            if (value <= 0) return;
            _addMoneyPool.GetObject<AddMoney>().Show(value);
        }
    }
}
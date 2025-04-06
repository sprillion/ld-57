using System;
using System.Collections.Generic;

namespace items
{
    public class ItemService
    {
        private readonly Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>();
        public static ItemService Instance;
        public event Action<ItemType> OnItemsCountChanged;

        public int Money { get; private set; } = 1000;

        public ItemService()
        {
            Instance = this;
            Clear();
        }

        public void Clear()
        {
            _items.Clear();
            foreach (ItemType value in Enum.GetValues(typeof(ItemType)))
            {
                _items.TryAdd(value, 0);
            }
        }

        public void AddItem(Item item)
        {
            _items[item.ItemType]++;
            Money += item.Price;
            item.Take();
            OnItemsCountChanged?.Invoke(item.ItemType);
        }

        public int GetItemCount(ItemType itemType)
        {
            return _items[itemType];
        }

        public bool RemoveMoney(int value)
        {
            if (value > Money) return false;
            Money -= value;
            return true;
        }
    }
}
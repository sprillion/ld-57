using System.Collections.Generic;
using System.Linq;
using items;
using UnityEngine;

namespace level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;

        [SerializeField] private List<Item> _items;

        public int LevelNumber => _levelNumber;


        public List<Item> GetAvailableItems()
        {
            return _items.Where(i => !i.Taken && i.CurrentTip == null).ToList();
        }
    }
}
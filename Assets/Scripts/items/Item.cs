﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using vfx;

namespace items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _value = 1;
            
        [SerializeField] private List<Transform> _pointsToCheck;

        public bool Taken { get; protected set; }

        public ItemType ItemType => _itemType;
        public int Price => _value;

        public Tip CurrentTip;

        public virtual void Take()
        {
            Taken = true;
            gameObject.SetActive(false);
            CurrentTip?.Release();
            CurrentTip = null;
        }

        public bool CheckFind()
        {
            return !Taken && _pointsToCheck.All(point =>
                Physics.Raycast(new Ray(point.position, Vector3.down), 2, LayerMask.GetMask("Ground")));
        }
    }
}
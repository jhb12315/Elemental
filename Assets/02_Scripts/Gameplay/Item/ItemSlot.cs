using System;
using UnityEngine;

namespace Elemental.Gameplay.item
{
    public class ItemSlot
    {
        ItemData itemData;

        public int ItemID => itemData.itemID;
        public int CurrentCount { get; private set; }

        public event Action<int> OnCountUp;
        public event Action<Sprite, int> OnChangedItem;

        public ItemSlot(ItemData data)
        {
            itemData = data;

            CurrentCount = 0;
        }

        public void ItemCountUp()
        {
            CurrentCount++;
            OnCountUp?.Invoke(CurrentCount);
        }

        public void ItemChanged(int count, ItemData data)
        {
            itemData = data;
            CurrentCount = count;

            OnChangedItem?.Invoke(itemData.sprite, CurrentCount);
        }
    }
}
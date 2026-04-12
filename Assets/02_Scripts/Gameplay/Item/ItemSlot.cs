using System;
using UnityEngine;

namespace Elemental.Gameplay.Item
{
    public class ItemSlot
    {
        ItemData itemData;

        public int ItemID { get; private set; }
        public int CurrentCount { get; private set; }

        public event Action<int> OnCountUp;
        public event Action OnItemNull;
        public event Action<Sprite, int> OnChangedItem;

        public ItemSlot(ItemData data)
        {
            itemData = data;
            ItemID = data.itemID;

            CurrentCount = 0;
        }

        public void ItemCountUp()
        {
            CurrentCount++;
            OnCountUp?.Invoke(CurrentCount);
        }

        public void RemoveItem(int removeCount)
        {
            CurrentCount -= removeCount;
            if (CurrentCount == 0)
            {
                OnItemNull?.Invoke();
            }
        }

        public void ItemChanged(int count, ItemData data)
        {
            itemData = data;
            CurrentCount = count;

            OnChangedItem?.Invoke(itemData.sprite, CurrentCount);
        }
    }
}
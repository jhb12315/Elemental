using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elemental.Gameplay.Item
{
    public class ItemSlotUI : MonoBehaviour
    {
        ItemSlot slot;
        Sprite nullSprite;
        Image itemSprite;
        TextMeshProUGUI itemCountText;

        void Awake()
        {
            itemSprite = transform.Find("ItemSprite").GetComponent<Image>();
            itemCountText = GetComponentInChildren<TextMeshProUGUI>();
            nullSprite = itemSprite.sprite;
            itemCountText.text = null;
        }

        public void SetUpSlotUI(ItemSlot slot)
        {
            this.slot = slot;
            slot.OnCountUp += CountUpdate;
            slot.OnItemNull += SetNull;
            slot.OnChangedItem += ItemChanged;
        }

        void CountUpdate(int count)
        {
            itemCountText.text = $"{count}";
        }

        void ItemChanged(Sprite sprite, int count)
        {
            itemSprite.sprite = sprite;
            itemCountText.text = $"{count}";
        }

        public void SetNull()
        {
            itemSprite.sprite = nullSprite;
            itemCountText.text = null;
        }

        void OnDestroy()
        {
            if (slot == null) return;
            slot.OnCountUp -= CountUpdate;
            slot.OnChangedItem -= ItemChanged;
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elemental.Gameplay.Resource.item.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        Image itemSprite;
        TextMeshProUGUI itemCountText;

        void Awake()
        {
            itemSprite = GetComponent<Image>();
            itemCountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetSprite(Sprite sprite, int count)
        {
            itemSprite.sprite = sprite;
            itemCountText.text = $"{count}";
        }

        public void SetNull()
        {
            itemSprite.sprite = null;
            itemCountText.text = null;
        }
    }
}
using Elemental.Framework.UI;
using Elemental.Gameplay.item.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.item
{
    public class Inventory : MonoBehaviour, ICraftable, IUIPanel
    {
        List<ItemSlot> itemSlots;
        ItemSlotUI[] itemSlotUI;

        int maxInventorySlotCount;
        int currentInventorySlotCount;

        void Awake()
        {
            currentInventorySlotCount = 0;
        }

        void Start()
        {
            itemSlotUI = GetComponentsInChildren<ItemSlotUI>();
            maxInventorySlotCount = itemSlotUI.Length;
            itemSlots = new List<ItemSlot>(maxInventorySlotCount);
            gameObject.SetActive(false);
        }

        // 체크 후 생성
        public bool TryCraft(List<RecipeIngredient> ingredients)
        {
            List<(ItemSlot, int)> removeCraftItem = new List<(ItemSlot, int)>(20);

            foreach (var ingredient in ingredients)
            {
                int id = ingredient.item.itemID;
                int requiredCount = ingredient.count;
                int currentCount = 0;

                foreach (var slot in itemSlots)
                {
                    if (slot.ItemID != id) continue;
                    currentCount += slot.CurrentCount;
                    if (currentCount < requiredCount)
                    {
                        removeCraftItem.Add((slot, slot.CurrentCount));
                        continue;
                    }
                    else if (currentCount >= requiredCount)
                    {
                        int removeCount = currentCount - requiredCount == 0 ? slot.CurrentCount : slot.CurrentCount - (currentCount - requiredCount);
                        removeCraftItem.Add((slot, removeCount));
                        break;
                    }
                }

                if (currentCount < requiredCount) return false;
            }

            Craft(removeCraftItem);

            return true;
        }

        void Craft(List<(ItemSlot, int)> removeCraftItem)
        {
            foreach (var (slot, count) in removeCraftItem)
            {
                slot.RemoveItem(count);
            }
        }

        // 재료가 충분한지 체크만
        public bool CanCraft(List<RecipeIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                int id = ingredient.item.itemID;
                int requiredCount = ingredient.count;
                int currentCount = 0;

                foreach (var slot in itemSlots)
                {
                    if (slot.ItemID != id) continue;
                    currentCount += slot.CurrentCount;
                    if (currentCount < requiredCount)
                    {
                        continue;
                    }
                    else if (currentCount >= requiredCount)
                    {
                        break;
                    }
                }

                if (currentCount < requiredCount) return false;
            }

            return true;
        }

        public void AddItem(ItemData item, ItemReturner returner)
        {
            ItemSlot addItemSlot = null;

            foreach (var slot in itemSlots)
            {
                if (slot.ItemID == item.itemID)
                {
                    if (slot.CurrentCount >= item.maxStackCount) continue;
                    addItemSlot = slot;
                    break;
                }
            }

            if (addItemSlot == null)
            {
                if (currentInventorySlotCount >= maxInventorySlotCount) return;
                addItemSlot = new ItemSlot(item);
                itemSlotUI[currentInventorySlotCount].SetUpSlotUI(addItemSlot);
                itemSlots.Add(addItemSlot);
                addItemSlot.ItemChanged(addItemSlot.CurrentCount, item);
                currentInventorySlotCount++;
            }

            addItemSlot.ItemCountUp();
            returner.OnCollected();
        }

        public void OnInventory(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if (UIManager.Instance.CurrentOpenUI != this as IUIPanel)
                {
                    UIManager.Instance.OpenUI(this);
                }
                else
                {
                    UIManager.Instance.CloseUI();
                }
            }
        }

        public void OnUI()
        {
            gameObject.SetActive(true);
        }

        public void OffUI()
        {
            gameObject.SetActive(false);
        }
    }
}
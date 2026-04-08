using Elemental.Gameplay.item.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.item
{
    public class Inventory : MonoBehaviour
    {
        List<ItemSlot> itemSlots;
        ItemSlotUI[] itemSlotUI;

        List<(ItemSlot, int)> removeCraftItem = new List<(ItemSlot, int)>(10);

        int maxInventorySlotCount;
        int currentInventorySlotCount;

        bool isOnInventory;

        void Awake()
        {
            currentInventorySlotCount = 0;
        }

        void Start()
        {
            itemSlotUI = GetComponentsInChildren<ItemSlotUI>();
            maxInventorySlotCount = itemSlotUI.Length;
            itemSlots = new List<ItemSlot>(maxInventorySlotCount);
            isOnInventory = false;
            gameObject.SetActive(false);
        }

        // TODO : 재료 확인과 삭제 분리
        public bool HasRecipeIngredients(List<RecipeIngredient> ingredients)
        {
            removeCraftItem.Clear();

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

            ExecuteCraft();

            return true;
        }

        void ExecuteCraft()
        {
            foreach (var (slot, count) in removeCraftItem)
            {
                slot.RemoveItem(count);
            }
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
                if (isOnInventory) isOnInventory = false;
                else isOnInventory = true;
                gameObject.SetActive(isOnInventory);
            }
        }
    }
}
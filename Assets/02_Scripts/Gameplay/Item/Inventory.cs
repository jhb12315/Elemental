using Elemental.Gameplay.item.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.item
{
    public class Inventory : MonoBehaviour
    {
        List<ItemSlot> slots;
        ItemSlotUI[] itemSlotUI;

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
            slots = new List<ItemSlot>(maxInventorySlotCount);
            isOnInventory = false;
            gameObject.SetActive(false);
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

        public void AddItem(ItemData item, ItemReturner returner)
        {
            ItemSlot addItemSlot = null;

            foreach (var slot in slots)
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
                slots.Add(addItemSlot);
                addItemSlot.ItemChanged(addItemSlot.CurrentCount, item);
                currentInventorySlotCount++;
            }

            addItemSlot.ItemCountUp();
            returner.OnCollected();
        }
    }
}
using Elemental.Gameplay.Resource.item.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay.Resource.item
{
    public class Inventory : MonoBehaviour
    {
        Dictionary<int, List<ItemSlot>> currentItems;
        [SerializeField]GameObject InventoryGridUI;
        ItemSlotUI[] itemSlotUI;

        [SerializeField] int maxInventorySlotCount;
        int currentInventorySlotCount;

        void Awake()
        {
            currentItems = new Dictionary<int, List<ItemSlot>>(maxInventorySlotCount);
            itemSlotUI = InventoryGridUI.GetComponentsInChildren<ItemSlotUI>();
        }

        void AddInventoryItem(ItemDataStorage item, ItemReturner returner)
        {
            if (currentItems.TryGetValue(item.ItemData.itemID, out List<ItemSlot> itemSlots))
            {
                ItemSlot slot = null;

                foreach (var itemSlot in itemSlots)
                {
                    if (itemSlot.CurrentCount < itemSlot.MaxOverlapCount)
                    {
                        slot = itemSlot;
                        break;
                    }
                }

                // 공간이 남은 리스트가 없을 때
                if (slot == null)
                {
                    if (currentInventorySlotCount == maxInventorySlotCount) return;

                    slot = new ItemSlot(item.ItemData);
                    itemSlots.Add(slot);
                    currentInventorySlotCount++;
                }

                slot.AddItem();
                returner.OnCollected();
            }
            // 딕셔너리에 없을 때
            else
            {
                if (currentInventorySlotCount == maxInventorySlotCount) return;

                ItemSlot slot = new ItemSlot(item.ItemData);
                List<ItemSlot> newItemSlots = new List<ItemSlot>();
                newItemSlots.Add(slot);

                currentItems.Add(item.ItemData.itemID, newItemSlots);
                currentInventorySlotCount++;
                slot.AddItem();
                returner.OnCollected();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("DropItem"))
            {
                AddInventoryItem(collision.gameObject.GetComponent<ItemDataStorage>(), collision.gameObject.GetComponent<ItemReturner>());
            }
        }
    }
}
using Elemental.Gameplay.item;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropItem"))
        {
            inventory.AddItem(collision.gameObject.GetComponent<ItemDataStorage>().ItemData, collision.gameObject.GetComponent<ItemReturner>());
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Framework.UI.Inventory
{
    public class InventoryOnOff : MonoBehaviour
    {
        bool isOnInventory;

        void Start()
        {
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
    }
}

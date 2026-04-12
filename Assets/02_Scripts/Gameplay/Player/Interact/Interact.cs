using Elemental.Framework.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.Player
{
    public class Interact : MonoBehaviour
    {
        Vector2 mousePosition;

        public void OnMousePosition(InputAction.CallbackContext ctx)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if (UIManager.Instance.IsMouseOnUI) return;

                Collider2D overlapObejct = Physics2D.OverlapPoint(mousePosition);

                if (overlapObejct == null)
                {
                    return;
                }

                else if (overlapObejct.gameObject.TryGetComponent(out IInteractable interactTarget))
                {
                    interactTarget.OnInteracted();
                }
            }
        }
    }
}
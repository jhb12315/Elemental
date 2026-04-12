using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.Player
{
    public class PlayerHarvest : MonoBehaviour
    {
        public bool canHarvest;
        ICuttable targetResource;

        void Awake()
        {
            canHarvest = false;
        }

        // TODO : 2개 이상의 자원이 붙었을 때 한 자리에서 채집 가능하게
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Resource")) return;
            targetResource = collision.gameObject.GetComponent<ICuttable>();
            canHarvest = true;
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Resource")) return;
            canHarvest = false;
            targetResource = null;
        }

        public void OnHarvest(InputAction.CallbackContext ctx)
        {
            if (!canHarvest) return;
            if (ctx.started) targetResource.Cut();
        }
    }
}
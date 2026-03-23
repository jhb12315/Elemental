using Elemental.Gameplay.Resource.Cut;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.Player.Harvest
{
    public class PlayerHarvest : MonoBehaviour
    {
        public bool canHarvest;
        ICuttable targetResource;

        void Awake()
        {
            canHarvest = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Resource")) return;
            targetResource = collision.gameObject.GetComponent<ICuttable>();
            canHarvest = true;
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Resource")) return;
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
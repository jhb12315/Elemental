using UnityEngine;
using UnityEngine.InputSystem;

namespace Elemental.Gameplay.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rigid;

        [SerializeField] float speed;

        Vector2 moveInput;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            rigid.linearVelocity = speed * moveInput;
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) moveInput = ctx.ReadValue<Vector2>();

            if (ctx.canceled) moveInput = Vector2.zero;
        }
    }
}
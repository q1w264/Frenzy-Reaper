using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEvent
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Vector2 _input;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void OnMoveEvent(InputAction.CallbackContext ctx)
        {
            _input = ctx.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.linearVelocity = _input * speed;
        }
    }
}
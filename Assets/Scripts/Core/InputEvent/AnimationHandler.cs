using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEvent
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AnimationHandler : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _aimDirection = Vector2.down;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void OnMoveEvent(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            if (direction.x != 0 || direction.y != 0)
                _aimDirection = direction.normalized;
        }

        private void Update()
        {
            float currentSpeed = _rigidbody2D.linearVelocity.magnitude;
            _animator.SetFloat(Speed, currentSpeed);
            _animator.SetFloat(MoveX, _aimDirection.x);
            _animator.SetFloat(MoveY, _aimDirection.y);

        }
    }
}

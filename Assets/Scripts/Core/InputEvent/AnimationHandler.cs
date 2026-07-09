using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEvent
{
    [RequireComponent(typeof(Animator))]
    public class AnimationHandler : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Animator _animator;
        private Vector2 _aimDirection = Vector2.down;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnMoveEvent(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
                _aimDirection = direction;
        }

        private void Update()
        {
            _animator.SetFloat(Speed, _aimDirection.magnitude);
            if (_aimDirection.magnitude > 0)
            {
                _animator.SetFloat(MoveX, _aimDirection.normalized.x);
                _animator.SetFloat(MoveY, _aimDirection.normalized.y);
            }
        }
    }
}

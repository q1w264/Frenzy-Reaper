using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEvent
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health.Health))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimationHandler : MonoBehaviour
    {
        public Color damageColor = Color.red;
        public float flashDuration = 0.1f;
        
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Animator _animator;
        private SpriteRenderer _renderer;
        private Health.Health _health;
        private Vector2 _aimDirection = Vector2.down;
        private Color _originalColor;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health.Health>();
            _renderer = GetComponent<SpriteRenderer>();
            _health.OnDamaged += OnDamaged;
            _originalColor = _renderer.color;
        }

        private void OnDamaged()
        {
            StartCoroutine(FlashColor());
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
        
        private IEnumerator FlashColor()
        {
            // 记录原始颜色（通常是纯白）
            
        
            // 变色
            _renderer.color = damageColor;
        
            // 等待短暂时间
            yield return new WaitForSeconds(flashDuration);
        
            // 恢复原色
            _renderer.color = _originalColor;
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnDamaged -= OnDamaged;
            }
        }
    }
}

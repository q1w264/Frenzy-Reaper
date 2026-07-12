using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Core.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Health.Health))]
    public class AnimationHandler : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private Animator _animator;
        private AIPath _aiPath;
        private Vector2 _aimDirection = Vector2.down;

        [Header("Animation")] [SerializeField] private float animationInterval = 0.15f; // 缓冲时间
        private float _nextDecisionTime;

        private SpriteRenderer _renderer;
        private Health.Health _health;
        private Color _originalColor;

        [Header("Damage")] public Color damageColor = Color.red;
        public float flashDuration = 0.1f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _aiPath = GetComponent<AIPath>();
            _health = GetComponent<Health.Health>();
            _renderer = GetComponent<SpriteRenderer>();
            _originalColor = _renderer.color;

            _health.OnDamaged += OnDamage;
            _health.OnDeath += OnDeath;
        }

        public void OnAttack()
        {
            _animator.SetTrigger(Attack);
        }

        private void OnDamage()
        {
            if (!_health.IsDead())
                StartCoroutine(FlashColor());
        }

        private void OnDeath()
        {
            _animator?.SetTrigger(Dead);
        }

        private void Update()
        {
            if (Time.time < _nextDecisionTime)
            {
                return;
            }

            _nextDecisionTime = Time.time + animationInterval;


            var direction = _aiPath.desiredVelocity;
            if ((direction.x != 0 || direction.y != 0) && Mathf.Abs(direction.x - _aimDirection.x) > 0.4f &&
                Mathf.Abs(direction.y - _aimDirection.y) > 0.4f)
                _aimDirection = direction.normalized;
            var currentSpeed = direction.magnitude;
            if (!_health.IsDead())
                _animator.SetFloat(Speed, currentSpeed);
            if (!_health.IsDead())
                _animator.SetFloat(MoveX, _aimDirection.x);
            if (!_health.IsDead())
                _animator.SetFloat(MoveY, _aimDirection.y);
        }

        private IEnumerator FlashColor()
        {
            // 变色
            _renderer.color = damageColor;

            // 等待短暂时间
            yield return new WaitForSeconds(flashDuration);

            // 恢复原色
            _renderer.color = _originalColor;
        }
    }
}
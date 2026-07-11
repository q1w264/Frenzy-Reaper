using Pathfinding;
using UnityEngine;

namespace Core.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AIPath))]
    public class AnimationHandler : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private Animator _animator;
        private AIPath _aiPath;
        private Vector2 _aimDirection = Vector2.down;
        
        [SerializeField] private float animationInterval = 0.15f; // 缓冲时间
        private float _nextDecisionTime;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _aiPath = GetComponent<AIPath>();
        }

        public void OnAttack()
        {
            _animator.SetTrigger(Attack);
        }

        private void Update()
        {
            if (Time.time < _nextDecisionTime)
            {
                return;
            }
            _nextDecisionTime = Time.time + animationInterval;

            var direction = _aiPath.desiredVelocity;
            if ((direction.x != 0 || direction.y != 0 )&& Mathf.Abs(direction.x - _aimDirection.x) > 0.4f &&
                Mathf.Abs(direction.y - _aimDirection.y) > 0.4f)
                _aimDirection = direction.normalized;
            var currentSpeed = direction.magnitude;

            _animator.SetFloat(Speed, currentSpeed);
            _animator.SetFloat(MoveX, _aimDirection.x);
            _animator.SetFloat(MoveY, _aimDirection.y);
        }
    }
}
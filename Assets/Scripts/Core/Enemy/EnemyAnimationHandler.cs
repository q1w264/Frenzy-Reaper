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
        private Animator _animator;
        private AIPath _aiPath;
        private Vector2 _aimDirection = Vector2.down;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _aiPath = GetComponent<AIPath>();
        }
        
        private void Update()
        {
            var direction = _aiPath.desiredVelocity;
            if (direction.x != 0 || direction.y != 0)
                _aimDirection = direction.normalized;
            var currentSpeed = direction.magnitude;
            
            _animator.SetFloat(Speed, currentSpeed);
            _animator.SetFloat(MoveX, _aimDirection.x);
            _animator.SetFloat(MoveY, _aimDirection.y);
        }
    }
}
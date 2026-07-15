using UnityEngine;

namespace Core.Animation.State.Default
{
    public class IdleState : IAnimationState
    {
        private readonly int _hash = Animator.StringToHash("Idle");
        
        public IAnimationState HandleInput(AnimationInput input)
        {
            return input switch
            {
                { isAttack: true } => new AttackState(),
                { speed: > 0f } => new MoveState(),
                { isDead: true } => new DeadState(),
                _ => null
            };
        }

        public void Animate(Animator animator)
        {
            animator.Play(_hash);
        }
    }
}
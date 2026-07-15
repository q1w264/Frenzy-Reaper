using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Animation.State.Default
{
    public class AttackState : IAnimationState<DefaultInput>
    {
        private readonly int _hash = Animator.StringToHash("Attack");

        [return: MaybeNull]
        public IAnimationState<DefaultInput> HandleInput(DefaultInput input)
        {
            return input switch
            {
                { isAttack: false, speed: > 0f } => new MoveState(),
                { isAttack: false, speed: 0f } => new IdleState(),
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
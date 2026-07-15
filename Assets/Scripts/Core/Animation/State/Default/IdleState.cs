using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Animation.State.Default
{
    public class IdleState : IAnimationState<DefaultInput>
    {
        private readonly int _hash = Animator.StringToHash("Idle");

        [return: MaybeNull]
        public IAnimationState<DefaultInput> HandleInput(DefaultInput input)
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
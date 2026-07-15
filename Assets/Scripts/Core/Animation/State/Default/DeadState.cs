using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Animation.State.Default
{
    public class DeadState : IAnimationState<DefaultInput>
    {
        private readonly int _hash = Animator.StringToHash("Dead");

        [return: MaybeNull]
        public IAnimationState<DefaultInput> HandleInput(DefaultInput input)
        {
            return null; // Dead state does not transition to any other state
        }

        public void Animate(Animator animator)
        {
            animator.Play(_hash);
        }
    }
}
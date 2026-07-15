using UnityEngine;

namespace Core.Animation.State.Default
{
    public class DeadState : IAnimationState
    {
        private readonly int _hash = Animator.StringToHash("Dead");
        
        public IAnimationState HandleInput(AnimationInput input)
        {
            return null; // Dead state does not transition to any other state
        }

        public void Animate(Animator animator)
        {
            animator.Play(_hash);
        }
    }
}
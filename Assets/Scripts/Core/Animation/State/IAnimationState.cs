using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Animation.State
{
    public interface IAnimationState
    {
        [return: MaybeNull]
        public IAnimationState HandleInput(AnimationInput  input);

        public void Animate(Animator animator);
    }
}
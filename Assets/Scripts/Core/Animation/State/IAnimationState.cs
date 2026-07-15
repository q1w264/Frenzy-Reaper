using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Animation.State
{
    public interface IAnimationState<in TInput> where TInput : struct, IAnimationInput
    {
        [return: MaybeNull]
        public IAnimationState<TInput> HandleInput(TInput input);

        public void Animate(Animator animator);
    }
}
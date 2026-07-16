using UnityEngine;

namespace Core.Animation
{
    [RequireComponent(typeof(Animator))]
    public abstract class BaseAnimationController : MonoBehaviour
    {
        protected Animator animator;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}
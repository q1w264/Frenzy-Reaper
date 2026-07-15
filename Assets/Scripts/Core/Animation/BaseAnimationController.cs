using UnityEngine;

namespace Core.Animation
{
    [RequireComponent(typeof(Animator))]
    public abstract class BaseAnimationController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
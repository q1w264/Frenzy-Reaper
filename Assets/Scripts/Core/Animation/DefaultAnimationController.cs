using SO.Event;
using UnityEngine;

namespace Core.Animation
{
    public class DefaultAnimationController : BaseAnimationController
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        
        [Header("Received SO Event")] 
        [SerializeField] private VoidSOEvent deadVoidSOEvent;
        [SerializeField] private VoidSOEvent damagedVoidSOEvent;
        [SerializeField] private VoidSOEvent attackVoidSOEvent;
        [SerializeField] private Vector2SOEvent facedDirectionVector2SOEvent;
        [SerializeField] private FloatSOEvent speedFloatSOEvent;
        
        
        private void Start()
        {
            animator.SetFloat(MoveX, 0f);
            animator.SetFloat(MoveY, -1f);
        }

        private void OnEnable()
        {
            deadVoidSOEvent.OnEvent += OnDeadEvent;
            damagedVoidSOEvent.OnEvent += OnDamagedEvent;
            attackVoidSOEvent.OnEvent += OnAttackEvent;
            facedDirectionVector2SOEvent.OnEvent += OnChangeDirectionEvent;
            speedFloatSOEvent.OnEvent += OnSpeedChangedEvent;
        }

        private void OnDisable()
        {
            deadVoidSOEvent.OnEvent -= OnDeadEvent;
            damagedVoidSOEvent.OnEvent -= OnDamagedEvent;
            attackVoidSOEvent.OnEvent -= OnAttackEvent;
            facedDirectionVector2SOEvent.OnEvent -= OnChangeDirectionEvent;
            speedFloatSOEvent.OnEvent -= OnSpeedChangedEvent;
        }
        
        private void OnDeadEvent()
        {
            //TODO
        }

        private void OnDamagedEvent()
        {
            //TODO
        }

        private void OnAttackEvent()
        {
            //TODO
        }

        private void OnChangeDirectionEvent(Vector2 direction)
        {
            animator.SetFloat(MoveX, direction.x);
            animator.SetFloat(MoveY, direction.y);
        }

        private void OnSpeedChangedEvent(float speed)
        {
            //TODO
        }
    }
}
using System.Threading;
using UnityEngine;

namespace Core.Animation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseAnimationController : MonoBehaviour
    {
        protected Animator animator;
        private SpriteRenderer  _spriteRenderer;
        private Color _defaultColor;
        
        [Header("Damaged Events")]
        [SerializeField]private Color damagedColor = Color.red;
        [SerializeField] private float lastDamagedAnimationTime = 0.5f;
        
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.color;
        }
        
        protected async Awaitable Damaged(CancellationToken cancellationToken)
        {
            _spriteRenderer.color = damagedColor;
            
            // 如果在这物体被隐藏了，这里会直接抛出 OperationCanceledException 异常退出，绝对不往下走
            await Awaitable.WaitForSecondsAsync(lastDamagedAnimationTime, cancellationToken);
            
            _spriteRenderer.color = _defaultColor;
        }
        
        protected void ClearColor()
        {
            _spriteRenderer.color = _defaultColor;
        }
        
    }
}
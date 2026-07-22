using System;
using System.Threading;
using SO.Event;
using UnityEngine;

namespace Core.Animation
{
    public class PlayerAnimationController : BaseAnimationController
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private CancellationTokenSource _cancellationTokenSource;
        
        [Header("Received SO Event")] [SerializeField]
        private VoidSOEvent playerDeadVoidSOEvent;
        [SerializeField] private VoidSOEvent playerDamagedVoidSOEvent;
        [SerializeField] private VoidSOEvent playerAttackVoidSOEvent;
        [SerializeField] private Vector2SOEvent playerFacedDirectionVector2SOEvent;
        [SerializeField] private FloatSOEvent playerSpeedFloatSOEvent;

        [Header("Settings")]
        [Tooltip(
            "The epsilon to judge whether the player is moving or not. If the speed is less than this value, the player is considered to be idle.")]
        [SerializeField]
        private float speedEpsilon = 0.01f;

        private void Start()
        {
            animator.SetFloat(MoveX, 0f);
            animator.SetFloat(MoveY, -1f);
        }

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            playerDeadVoidSOEvent.OnEvent += OnPlayerDeadEvent;
            playerDamagedVoidSOEvent.OnEvent += OnPlayerDamagedEvent;
            playerAttackVoidSOEvent.OnEvent += OnPlayerAttackEvent;
            playerFacedDirectionVector2SOEvent.OnEvent += OnChangeDirectionEvent;
            playerSpeedFloatSOEvent.OnEvent += OnPlayerSpeedChangedEvent;
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            
            playerDeadVoidSOEvent.OnEvent -= OnPlayerDeadEvent;
            playerDamagedVoidSOEvent.OnEvent -= OnPlayerDamagedEvent;
            playerAttackVoidSOEvent.OnEvent -= OnPlayerAttackEvent;
            playerFacedDirectionVector2SOEvent.OnEvent -= OnChangeDirectionEvent;
            playerSpeedFloatSOEvent.OnEvent -= OnPlayerSpeedChangedEvent;
        }

        private void OnPlayerDeadEvent()
        {
            animator.Play("Death");
        }

        private async void OnPlayerDamagedEvent()
        {
            try
            {
                await Damaged(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                ClearColor();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in OnPlayerDamagedEvent: {e.Message}");
            }
        }

        private void OnPlayerAttackEvent()
        {
            //TODO 完成角色相关的动画
        }

        private void OnChangeDirectionEvent(Vector2 direction)
        {
            animator.SetFloat(MoveX, direction.x);
            animator.SetFloat(MoveY, direction.y);
        }

        private void OnPlayerSpeedChangedEvent(float speed)
        {
            animator.Play(Mathf.Abs(speed) > speedEpsilon ? "Walk" : "Idle");
        }
    }
}
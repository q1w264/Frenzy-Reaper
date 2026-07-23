using UnityEngine;
using Input;
using SO.Event;
using UnityEngine.InputSystem;

namespace Core.Characters.Player
{
    public class InputHandler : MonoBehaviour, GameInputSystem.IPlayerActions
    {
        private GameInputSystem _gameInputSystem;
        private GameInputSystem.PlayerActions _playerActions;

        [Header("Events")] [SerializeField] private Vector2SOEvent playerMoveEvent;
        [SerializeField] private VoidSOEvent playerAttackEvent;

        private void Awake()
        {
            _gameInputSystem = new GameInputSystem();
            _playerActions = _gameInputSystem.Player;
            _playerActions.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _playerActions.Enable();
        }

        private void OnDisable()
        {
            _playerActions.Disable();
        }

        private void OnDestroy()
        {
            _gameInputSystem.Dispose();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            playerMoveEvent.InvokeEvent(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            playerAttackEvent.InvokeEvent();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
        }

        public void OnNext(InputAction.CallbackContext context)
        {
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
        }
    }
}
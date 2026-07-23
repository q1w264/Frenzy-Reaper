using SO.Event;
using UnityEngine;

namespace Core.Characters.Player
{
    public class Attack : MonoBehaviour
    {
        [Header("Events")] [SerializeField] private VoidSOEvent playerAttackEvent;

        private void OnEnable()
        {
            playerAttackEvent.OnEvent += OnAttackEvent;
        }

        private void OnDisable()
        {
            playerAttackEvent.OnEvent -= OnAttackEvent;
        }

        private void OnAttackEvent()
        {
            //TODO complete this method.
        }
        
        
    }
}
using System;
using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(fileName = "Void SO Event", menuName = "SO Event/Void SO Event", order = 4)]
    public class VoidSOEvent : ScriptableObject
    {
        private Action _event;

        public event Action OnEvent
        {
            add => _event += value;
            remove => _event -= value;
        }

        private void InvokeEvent()
        {
            _event?.Invoke();
        }
    }
}
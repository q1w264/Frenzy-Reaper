using System;
using UnityEngine;

namespace SO.Event
{
    public abstract class BaseSOEvent<T> : ScriptableObject
    {
        private Action<T> _event;

        public event Action<T> OnEvent
        {
            add => _event += value;
            remove => _event -= value;
        }

        public void InvokeEvent(T value)
        {
            _event?.Invoke(value);
        }
    }
}
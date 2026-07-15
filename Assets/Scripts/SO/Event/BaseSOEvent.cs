using System;
using UnityEngine;

namespace SO.Event
{
    public abstract class BaseSOEvent<T> : ScriptableObject
    {
        private Action<T> _event;

        event Action<T> OnEvent
        {
            add => _event += value;
            remove => _event -= value;
        }

        private void InvokeEvent(T value)
        {
            _event?.Invoke(value);
        }
    }
}
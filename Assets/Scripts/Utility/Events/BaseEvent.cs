using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Events
{
    public class BaseEvent<T> : ScriptableObject
    {
        event Action<T> OnEventRaised;
        public void RaiseEvent(T arg)
        {
            OnEventRaised?.Invoke(arg);
        }
    }
}

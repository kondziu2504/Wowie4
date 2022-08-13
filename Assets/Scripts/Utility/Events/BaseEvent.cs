using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Events
{
    public class BaseEvent<T> : ScriptableObject
    {
        [SerializeField] bool log = false;

        public event Action<T> OnEventRaised;

        public void RaiseEvent(T arg)
        {
            OnEventRaised?.Invoke(arg);
            if(log)
                Debug.Log($"{GetType()} event fired!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using System;

namespace Utility
{
    public class ExecuteOnMainThread : MonoBehaviour
    {
        static readonly ConcurrentQueue<Func<IEnumerator>> coroutinesQueue = new ConcurrentQueue<Func<IEnumerator>>();
        static readonly ConcurrentQueue<Action> actionsQueue = new ConcurrentQueue<Action>();

        public static void Run(Func<IEnumerator> coroutine)
        {
            coroutinesQueue.Enqueue(coroutine);
        }
        public static void Run(Action action)
        {
            actionsQueue.Enqueue(action);
        }

        void Update()
        {
            if (!coroutinesQueue.IsEmpty)
                while (coroutinesQueue.TryDequeue(out var action))
                    if (action != null)
                        StartCoroutine(action());

            if (!actionsQueue.IsEmpty)
                while (actionsQueue.TryDequeue(out var action))
                    action?.Invoke();
        }
    }
}


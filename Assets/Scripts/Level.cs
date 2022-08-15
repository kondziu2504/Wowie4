using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Events;

namespace Wowie4
{
    public class Level : MonoBehaviour
    {
        [SerializeField] VoidEvent playerDied;

        private void Awake()
        {
            playerDied.OnEventRaised += PlayerDied_OnEventRaised;
        }

        private void PlayerDied_OnEventRaised()
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

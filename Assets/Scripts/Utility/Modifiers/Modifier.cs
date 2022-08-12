using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Modifiers
{
    [System.Serializable]
    public class Modifier
    {
        [SerializeField] float value;

        public float Value => value;

        public Modifier(float value)
        {
            this.value = value;
        }
    }
}
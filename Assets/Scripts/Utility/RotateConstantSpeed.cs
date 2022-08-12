using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{

    public class RotateConstantSpeed : MonoBehaviour
    {
        [Tooltip("Degrees per second")]
        [SerializeField] float speed = 360f;

        void Update()
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }

}

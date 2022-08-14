using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    public class Diode : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Color offColor;

        Color originalColor;

        private void Awake()
        {
            Assert.IsNotNull(spriteRenderer);
            originalColor = spriteRenderer.color;
        }

        public void TurnOff()
        {
            spriteRenderer.color = offColor;
        }

        public void TurnOn()
        {
            spriteRenderer.color = originalColor;
        }
    }
}

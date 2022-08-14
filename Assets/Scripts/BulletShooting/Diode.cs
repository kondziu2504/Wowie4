using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    public class Diode : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite unlitSprite;

        Sprite originalSprite;

        private void Awake()
        {
            Assert.IsNotNull(spriteRenderer);
            originalSprite = spriteRenderer.sprite;
        }

        public void TurnOff()
        {
            spriteRenderer.sprite = unlitSprite;
        }

        public void TurnOn()
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}

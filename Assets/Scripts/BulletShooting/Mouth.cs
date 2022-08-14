using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mouth : MonoBehaviour
    {
        [SerializeField] RuntimeGameData runtimeGameData;

        [SerializeField] GameObject upperPart;
        [SerializeField] GameObject lowerPart;
        [SerializeField, Range(0.01f, 1f)] float openOffset = 0.2f;

        public System.Action OnGoodCodeEaten { get; set; }

        private bool MouthIsOpen => runtimeGameData.Energy > 0;

        private float lowerOriginalY;
        private float upperOriginalY;

        private void Awake()
        {
            Assert.IsTrue(GetComponentsInChildren<Collider2D>().Length > 0);
            Assert.IsNotNull(upperPart);
            Assert.IsNotNull(lowerPart);

            lowerOriginalY = lowerPart.transform.position.y;
            upperOriginalY = upperPart.transform.position.y;
        }

        private void Update()
        {
            lowerPart.transform.position = new Vector3(
                lowerPart.transform.position.x,
                lowerOriginalY - runtimeGameData.Energy * openOffset);

            upperPart.transform.position = new Vector3(
                upperPart.transform.position.x,
                upperOriginalY + runtimeGameData.Energy * openOffset);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!MouthIsOpen)
                return;

            var bullet = collision.GetComponent<Bullet>();

            if(bullet != null && bullet.BulletType == Bullet.Type.Good)
            {
                bullet.Destroy();
                OnGoodCodeEaten?.Invoke();
            }
        }
    }
}

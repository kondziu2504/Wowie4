using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mouth : MonoBehaviour
    {
        public System.Action OnGoodCodeEaten { get; set; }

        private void Awake()
        {
            Assert.IsTrue(GetComponentsInChildren<Collider2D>().Length > 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.GetComponent<Bullet>();

            if(bullet != null && bullet.BulletType == Bullet.Type.Good)
            {
                bullet.Destroy();
                OnGoodCodeEaten?.Invoke();
            }
        }
    }
}

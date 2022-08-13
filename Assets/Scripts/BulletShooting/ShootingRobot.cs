using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShootingRobot : MonoBehaviour
    {
        [SerializeField] BulletsShooter bulletsShooter;

        [SerializeField] int health = 3;
        [SerializeField] Diode[] diodes;

        private void Awake()
        {
            Assert.IsTrue(GetComponentsInChildren<Collider2D>().Length > 0);
            Assert.IsTrue(diodes.Length == health && diodes.All(diode => diode != null));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.Destroy();

                if(health > 0)
                {
                    diodes[diodes.Length - health].TurnOff();
                    health--;
                }
            }
        }
    }
}

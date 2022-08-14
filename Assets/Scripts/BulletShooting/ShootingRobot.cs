using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utility.Events;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShootingRobot : MonoBehaviour
    {
        [SerializeField] BulletsShooter bulletsShooter;
        [SerializeField] int maxHealth = 3;
        int currentHealth = 3;
        [SerializeField] Diode[] diodes;
        [SerializeField] Mouth mouth;
        [SerializeField] VoidEvent onGoodCodeEaten;
        [SerializeField] VoidEvent badCodePassed;

        private void Awake()
        {
            Assert.IsTrue(GetComponentsInChildren<Collider2D>().Length > 0);
            Assert.IsTrue(diodes.Length == currentHealth && diodes.All(diode => diode != null));
            Assert.IsNotNull(mouth);
            Assert.IsNotNull(onGoodCodeEaten);
            Assert.IsNotNull(badCodePassed);
        }

        private void Start()
        {
            mouth.OnGoodCodeEaten += OnGoodCodeEaten;
            badCodePassed.OnEventRaised += DealDamage;
        }

        private void OnGoodCodeEaten()
        {
            currentHealth = Mathf.Min(currentHealth + 1, maxHealth);
            diodes[currentHealth - 1].TurnOn();
            onGoodCodeEaten.RaiseEvent();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.GetComponent<Bullet>();
            if(bullet != null)
            {
                if(bullet.BulletType == Bullet.Type.Bad)
                {
                    DealDamage();
                    bullet.Destroy();
                }
                else if(bullet.BulletType == Bullet.Type.Neutral)
                {
                    bullet.Destroy();
                }
             
            }
        }

        private void DealDamage()
        {
            if (currentHealth > 0)
            {
                diodes[diodes.Length - currentHealth].TurnOff();
                currentHealth--;
            }
        }
    }
}

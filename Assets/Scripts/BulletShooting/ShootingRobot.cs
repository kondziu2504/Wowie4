using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utility.Events;
using DG.Tweening;

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
		[SerializeField] RuntimeGameData runtimeGameData;
		[SerializeField] Transform head;

		[SerializeField] float shakeStrength = 0.5f;
		[SerializeField] ShootingRobotParticles particles;
		[SerializeField] Transform healHalo;

		[SerializeField] GameObject[] eyes;
		[SerializeField] SpriteRenderer[] ears;
		[SerializeField] ParticleSystem[] earParticles;
		[SerializeField] AudioSource damageAudio;
		[SerializeField] VoidEvent playerDied;
		private Vector3 originalhaloScale;

		private void Awake()
		{
			Assert.IsTrue(GetComponentsInChildren<Collider2D>().Length > 0);
			Assert.IsTrue(diodes.Length == currentHealth && diodes.All(diode => diode != null));
			Assert.IsNotNull(mouth);
			Assert.IsNotNull(onGoodCodeEaten);
			Assert.IsNotNull(badCodePassed);

			originalhaloScale = healHalo.localScale;
		}

		private void Start()
		{
			mouth.OnGoodCodeEaten += OnGoodCodeEaten;
			badCodePassed.OnEventRaised += DealDamage;
			healHalo.localScale = Vector3.zero;
		}

        private void OnDestroy()
        {
			badCodePassed.OnEventRaised -= DealDamage;
		}

        private void Update()
		{
			foreach (var ear in ears)
				ear.color = runtimeGameData.Energy > 0 ? Color.white : Color.gray;

			foreach (var eye in eyes)
				eye.SetActive(runtimeGameData.Energy > 0);

			foreach (var earParticles in earParticles)
				if (runtimeGameData.Energy > 0)
				{
					if (earParticles.isStopped)
					{
						earParticles.Play();
					}
				}
				else if (!earParticles.isStopped)
				{
					earParticles.Stop();
				}
		}

		private void OnGoodCodeEaten()
		{
			int initialHealth = currentHealth;
			//currentHealth = Mathf.Min(currentHealth + 1, maxHealth);
			diodes[currentHealth - 1].TurnOn();
			onGoodCodeEaten.RaiseEvent();

			var seq = DOTween.Sequence();
			seq.Append(healHalo.DOScale(originalhaloScale, 0.3f).SetEase(Ease.OutSine));
			seq.Append(healHalo.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InSine));
			seq.Play();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var bullet = collision.GetComponent<Bullet>();
			if (bullet != null)
			{
				if (bullet.BulletType == Bullet.Type.Bad)
				{
					DealDamage();
					bullet.Destroy_();
				}
				else if (bullet.BulletType == Bullet.Type.Good)
				{
					bullet.Destroy_(true);
					OnGoodCodeEaten();
				}

			}
		}

		private void DealDamage()
		{
			if (currentHealth > 0 && !runtimeGameData.ShieldActive)
			{
				diodes[currentHealth - 1].TurnOff();
				currentHealth--;
				damageAudio?.Play();
				head.DOKill();
				head.DOShakePosition(0.5f, shakeStrength);
				particles.PlayDamage();
				if(currentHealth == 0)
                {
					var rigidbody = GetComponent<Rigidbody2D>();
					rigidbody.bodyType = RigidbodyType2D.Dynamic;
					rigidbody.AddForce(new Vector2(1, 4), ForceMode2D.Impulse);
					rigidbody.gravityScale = 3f;
					playerDied.RaiseEvent();
					enabled = false;
					bulletsShooter.active = false;
                }
			}
		}
	}
}

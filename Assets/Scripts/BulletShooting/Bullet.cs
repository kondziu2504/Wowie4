using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utility.Events;

namespace Wowie4
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] new Rigidbody2D rigidbody;
        [SerializeField] ParticleSystem numbersParticles;
        [SerializeField] ParticleSystem numbersExplosionPrefab;
        [SerializeField] SpriteRenderer halo;
        [SerializeField] RuntimeGameData runtimeGameData;

        [SerializeField] VoidEvent neutralCodePassed;
        [SerializeField] VoidEvent badCodePassed;

        public enum Type { Good, Bad, Neutral }

        public Type BulletType { get; private set; }
        public int Lane { get; private set; }

        #region Unity messages 

        private bool destroyed = false;

        private void Awake()
        {
            Assert.IsNotNull(rigidbody);
            Assert.IsNotNull(numbersParticles);
            Assert.IsNotNull(runtimeGameData);

            Assert.IsTrue(GetComponentInChildren<Collider2D>() != null);
            Assert.IsNotNull(neutralCodePassed);

            runtimeGameData.Bullets.Add(this);
        }

        public void OnBecameInvisible()
        {
            DestroyByPassing();
        }

        #endregion

        public void Init(Type actionType, Vector2 velocity, int lane)
        {
            this.BulletType = actionType;
            rigidbody.velocity = velocity;
            this.Lane = lane;
            var particlesColorOverLifetime = numbersParticles.colorOverLifetime;
            var gradient = new Gradient();
            gradient.colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(GetColor(), 0f),
                new GradientColorKey(new Color(0,0,0,0), 1f)
            };
            particlesColorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
            halo.color = GetColor();
        }

        private Color GetColor()
        {
            return BulletType switch
            {
                Type.Good => Color.green,
                Type.Bad => Color.red,
                Type.Neutral => Color.yellow,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public void Destroy_(bool useExplosion = false)
        {
            if (destroyed)
                return;

            runtimeGameData.Bullets.Remove(this);
            Destroy(gameObject);
            try
            {
                numbersParticles.transform.SetParent(null, true);
            }
            catch (Exception ex) { }
 
            numbersParticles.Stop();

            if (useExplosion)
            {
                var explosion = Instantiate(numbersExplosionPrefab, transform.position, Quaternion.identity);
                var main = explosion.main;
                main.startColor = new ParticleSystem.MinMaxGradient(GetColor());
                var trails = explosion.trails;
                trails.colorOverTrail = new ParticleSystem.MinMaxGradient(GetColor());
            }

            destroyed = true;
        }

        private void DestroyByPassing()
        {
            if (destroyed)
                return;

            if (BulletType == Type.Neutral)
                neutralCodePassed.RaiseEvent();
            else if (BulletType == Type.Bad)
                badCodePassed.RaiseEvent();
            Destroy_();
        }
    }
}


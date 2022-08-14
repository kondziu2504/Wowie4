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
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] RuntimeGameData runtimeGameData;

        [SerializeField] VoidEvent neutralCodePassed;
        [SerializeField] VoidEvent badCodePassed;

        public enum Type { Good, Bad, Neutral }

        public Type BulletType { get; private set; }

        #region Unity messages 

        private bool destroyed = false;

        private void Awake()
        {
            Assert.IsNotNull(rigidbody);
            Assert.IsNotNull(spriteRenderer);
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

        public void Init(Type actionType, Vector2 velocity)
        {
            this.BulletType = actionType;
            rigidbody.velocity = velocity;
            spriteRenderer.color = GetColor();
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

        public void Destroy()
        {
            if (destroyed)
                return;

            runtimeGameData.Bullets.Remove(this);
            Destroy(gameObject);

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
            Destroy();
        }
    }
}


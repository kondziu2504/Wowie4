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

        public enum Type { Good, Bad, Neutral }

        public Type BulletType { get; private set; }

        #region Unity messages 

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
            runtimeGameData.Bullets.Remove(this);
            Destroy(gameObject);
        }

        private void DestroyByPassing()
        {
            if(BulletType == Type.Neutral)
                neutralCodePassed.RaiseEvent();
            Destroy();
        }
    }
}


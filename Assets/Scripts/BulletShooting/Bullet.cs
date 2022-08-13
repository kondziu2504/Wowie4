using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wowie4
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] new Rigidbody2D rigidbody;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] RuntimeGameData runtimeGameData;

        public Action.Type ActionType { get; private set; }

        #region Unity messages 

        private void Awake()
        {
            Assert.IsNotNull(rigidbody);
            Assert.IsNotNull(spriteRenderer);
            Assert.IsNotNull(runtimeGameData);

            Assert.IsTrue(GetComponentInChildren<Collider2D>() != null);

            runtimeGameData.Bullets.Add(this);
        }

        public void OnBecameInvisible()
        {
            Destroy();
        }

        #endregion

        public void Init(Action.Type actionType, Vector2 velocity)
        {
            this.ActionType = actionType;
            rigidbody.velocity = velocity;
            spriteRenderer.color = Action.GetActionColor(actionType);
        }

        public void Destroy()
        {
            runtimeGameData.Bullets.Remove(this);
            Destroy(gameObject);
        }
    }
}


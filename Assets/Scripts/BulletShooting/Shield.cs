using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Events;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] ActionChosenEvent actionChosenEvent;
        [SerializeField] RuntimeGameData runtimeGameData;

        private void Awake()
        {
            actionChosenEvent.OnEventRaised += ShieldToggle;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (runtimeGameData.Energy == 0)
                ShieldToggle(Action.Type.Shields);
        }

        private void ShieldToggle(Action.Type actionType)
        {
            if (actionType != Action.Type.Shields)
                return;

            gameObject.SetActive(!gameObject.activeSelf);
            runtimeGameData.ShieldActive = gameObject.activeSelf;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.GetComponent<Bullet>();
            if (bullet != null && bullet.BulletType == Bullet.Type.Bad)
                bullet.Destroy_(true);
        }
    }
}

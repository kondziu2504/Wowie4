using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utility;
using UnityEngine.Assertions;

namespace Wowie4
{
    public class BulletSpawner : MonoBehaviour
    {
        [Header("Spawning")]
        [SerializeField] float spawnLength = 2f;
        [SerializeField, Range(0.1f, 10f)] float spawnPeriod = 1f;

        [Header("Bullets")]
        [SerializeField] Bullet bulletPrefab;
        [SerializeField, Range(0f, 10f)] float bulletSpeed = 1f;

        [Header("Other")]
        [SerializeField] RuntimeGameData runtimeGameData;

        #region Unity messages

        private void Awake()
        {
            Assert.IsNotNull(runtimeGameData);
        }

        private void Start()
        {
            StartCoroutine(SpawningCoro());
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                transform.position + Vector3.left * spawnLength / 2f,
                transform.position + Vector3.right * spawnLength / 2f);

            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.3f);
        }

        #endregion

        IEnumerator SpawningCoro()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnPeriod);
                SpawnBullet();
            }
        }

        private void SpawnBullet()
        {
            var bullet = Instantiate(bulletPrefab, GetRandomSpawnPos(), Quaternion.identity);
            bullet.Init(GetRandomActionType(), Vector2.down * bulletSpeed);
        }

        private Vector3 GetRandomSpawnPos()
        {
            var xOffset = UnityEngine.Random.Range(0, spawnLength) - spawnLength / 2f;
            return transform.position + Vector3.right * xOffset;
        }

        private Action.Type GetRandomActionType()
        {
            return Enum.GetValues(typeof(Action.Type)).Cast<Action.Type>().ToList().RandomElement();
        }
    }

}


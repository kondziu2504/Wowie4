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
        [SerializeField] Lanes lanes;

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
            bullet.Init(GetRandomBulletType(), Vector2.down * bulletSpeed);
        }

        private Vector3 GetRandomSpawnPos()
        {
            var xOffset = UnityEngine.Random.Range(0, spawnLength) - spawnLength / 2f;
            var randomLane = UnityEngine.Random.Range(0, lanes.MaxLanes);
            return new Vector3(lanes.GetLaneX(randomLane), transform.position.y) + Vector3.right * xOffset;
        }

        private Bullet.Type GetRandomBulletType()
        {
            return Enum.GetValues(typeof(Bullet.Type)).Cast<Bullet.Type>().ToList().RandomElement();
        }
    }

}


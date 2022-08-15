using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

namespace Wowie4
{
	public class BulletsShooter : MonoBehaviour
	{
		[SerializeField] RuntimeGameData runtimeGameData;
		[SerializeField] ActionChosenEvent actionChosenEvent;
		[SerializeField] AudioSource shootAduxio;
		[SerializeField] Transform eye1;
		[SerializeField] Transform eye2;

		[SerializeField] Laser laser1;
		[SerializeField] Laser laser2;

		[SerializeField] Lanes lanes;

		public bool active = true;

		private void Awake()
		{
			Assert.IsNotNull(runtimeGameData);
			actionChosenEvent.OnEventRaised += Shoot;
		}

        private void OnDestroy()
        {
			actionChosenEvent.OnEventRaised -= Shoot;
		}

        private void Shoot(Action.Type actionType)
		{
			if (!active || runtimeGameData.Energy == 0 || actionType != Action.Type.Shoot)
				return;

			var matchingBullet = runtimeGameData.Bullets
				.Where(x => x != null && x.Lane == lanes.CurrentLane && x.transform.position.y > transform.position.y)
				.OrderBy(x => x.transform.position.y).FirstOrDefault();
			if (matchingBullet != null)
			{
				var laserColor = Color.red; //Action.GetActionColor(actionType);
				laser1.Shoot(eye1.position, matchingBullet.transform.position, laserColor);
				laser2.Shoot(eye2.position, matchingBullet.transform.position, laserColor);
				shootAduxio?.Play();
				matchingBullet.Destroy_(true);
			}
		}

		private float DistanceTo(Vector3 target) => (transform.position - target).magnitude;
	}
}


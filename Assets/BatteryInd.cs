using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Events;

namespace Wowie4
{
	public class BatteryInd : MonoBehaviour
	{
		[SerializeField] LineRenderer lineRenderer;
		[SerializeField] float min = -3, max = 2.4f;
		[SerializeField] RuntimeGameData RuntimeGameData;
		[SerializeField] Color green, yellow, red;
		[SerializeField] VoidEvent playerDied;

        private void Awake()
        {
            playerDied.OnEventRaised += PlayerDied_OnEventRaised;
        }

        private void PlayerDied_OnEventRaised()
        {
			gameObject.SetActive(false);
        }

        private void Update()
		{
			lineRenderer.SetPosition(0, new Vector3(-3.6f, Mathf.Lerp(max, min, RuntimeGameData.Energy)));
			lineRenderer.startColor = RuntimeGameData.Energy > .66f ? green : RuntimeGameData.Energy > .33f ? yellow : red;
			lineRenderer.endColor = lineRenderer.startColor;
		}

	}
}

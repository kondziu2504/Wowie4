using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
	public class BatteryInd : MonoBehaviour
	{
		[SerializeField] LineRenderer lineRenderer;
		[SerializeField] float min = -3, max = 2.4f;
		[SerializeField] RuntimeGameData RuntimeGameData;
		[SerializeField] Color green, yellow, red;
		private void Update()
		{
			lineRenderer.SetPosition(0, new Vector3(-3.6f, Mathf.Lerp(max, min, RuntimeGameData.Energy)));
			lineRenderer.startColor = RuntimeGameData.Energy > .66f ? green : RuntimeGameData.Energy > .33f ? yellow : red;
			lineRenderer.endColor = lineRenderer.startColor;
		}

	}
}

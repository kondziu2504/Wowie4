using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
	public class BlinkObject : MonoBehaviour
	{
		public float blinkTime = .5f;
		public GameObject blinkObject;
		private bool isBlinking;
		private Coroutine blinkCoroutine;
		IEnumerator Blink()
		{
			isBlinking = true;
			yield return new WaitForSeconds(blinkTime);
			blinkObject.SetActive(true);
			yield return new WaitForSeconds(blinkTime);
			blinkObject.SetActive(false);
			blinkCoroutine = StartCoroutine(Blink());
		}

		public void StartBlinking()
		{
			if (isBlinking == false)
			{
				blinkCoroutine = StartCoroutine(Blink());
			}
		}
		public void StopBlinking()
		{
			isBlinking = false;
			if (blinkCoroutine is not null)
			{
				StopCoroutine(blinkCoroutine);
			}
			blinkObject.SetActive(false);
		}
	}
}

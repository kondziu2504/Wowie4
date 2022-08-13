using System;
using UnityEngine;

namespace Wowie4
{
	public class SwitchWall : MonoBehaviour
	{
		[SerializeField] SpriteRenderer spriteRenderer;
		[SerializeField] bool isEnabled;
		[SerializeField] ActionChosenEvent ActionChosenEvent;
		[SerializeField] Action.Type switchType;
		[SerializeField] BoxCollider2D boxCollider;

		private void OnEnable()
		{
			ActionChosenEvent.OnEventRaised += Switch;
		}

		private void OnDisable()
		{
			ActionChosenEvent.OnEventRaised -= Switch;
		}
		private void Switch(Action.Type obj)
		{
			if (obj == switchType)
			{
				isEnabled = !isEnabled;
				ChangeSprite();
				boxCollider.enabled = isEnabled;
			}
		}

		private void ChangeSprite()
		{
			Color c = spriteRenderer.color;
			c.a = isEnabled ? 1f : .5f;
			spriteRenderer.color = c;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility.Events;

namespace Wowie4
{
	public class WorldButton : MonoBehaviour
	{
		public UnityEvent<Action.Type> ButtonPressed;
		[SerializeField] Action.Type actionType;
		[SerializeField] Sprite pressedSprite;
		[SerializeField] SpriteRenderer spriteRenderer;

		Sprite defaultSprite;
		// Start is called before the first frame update
		void Start()
		{
			defaultSprite = spriteRenderer.sprite;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			ButtonPressed?.Invoke(actionType);

			spriteRenderer.sprite = pressedSprite;
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			spriteRenderer.sprite = defaultSprite;
		}
	}
}

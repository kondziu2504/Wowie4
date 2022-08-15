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

		[SerializeField] SpriteRenderer actionBackground;
		[SerializeField] GameObject halo;
		[SerializeField] RuntimeGameData runtimeGameData;

		Color originalActionIconColor;

		Sprite defaultSprite;
		// Start is called before the first frame update
		void Start()
		{
			defaultSprite = spriteRenderer.sprite;
			originalActionIconColor = actionBackground.color;
		}

        private void Update()
        {
			bool hasEnergy = runtimeGameData.Energy > 0;
			halo.SetActive(hasEnergy);
			actionBackground.color = hasEnergy ? originalActionIconColor : Color.gray;
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

using UnityEngine;
using Utility.Events;

namespace Wowie4
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Elevator : MonoBehaviour
	{
		[SerializeField] float maxHeight = 2f;
		[SerializeField] float descendingSpeed = 1f;
		[SerializeField] float ascendingSpeed = 1f;
		[SerializeField] float descendBoostMultiplier = 2f;
		[SerializeField] RuntimeGameData runtimeGameData;
		[SerializeField] BlinkObject blinkObject;
		[SerializeField] BlinkObject lowBatteryBlink;
		[SerializeField] AudioSource dischargedAudio;
		[SerializeField] VoidEvent playerDied;
		private float originalHeight;
		private bool discharged, lastFrameDischarged;
		private float BottomHeight => Application.isPlaying ? originalHeight : transform.localPosition.y;
		private float TopHeight => BottomHeight + maxHeight;

		private bool descending = false;

		protected Controls controls;

		private new Rigidbody2D rigidbody;

		private void Awake()
		{
			originalHeight = transform.localPosition.y;
			rigidbody = GetComponent<Rigidbody2D>();
            playerDied.OnEventRaised += PlayerDied_OnEventRaised;
		}

        private void PlayerDied_OnEventRaised()
        {
			enabled = false;
			gameObject.SetActive(false);
        }

        private void OnEnable()
		{
			controls = new Controls();
			controls.Enable();
		}

		private void OnDisable()
		{
			controls.Disable();
		}

		private void FixedUpdate()
		{
			if (!descending)
				Ascend();
		}

		private void Update()
		{
			runtimeGameData.Energy = Mathf.InverseLerp(TopHeight, BottomHeight, transform.localPosition.y);
			if (runtimeGameData.Energy < 0.02f)
			{
				discharged = true;
				lowBatteryBlink.StartBlinking();
			}
			else
			{
				discharged = false;
				lowBatteryBlink.StopBlinking();
			}
			if (lastFrameDischarged == false && discharged == true)
			{
				dischargedAudio.Play();
			}
			lastFrameDischarged = discharged;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;

			Gizmos.DrawLine(
				new Vector3(transform.position.x, BottomHeight),
				new Vector3(transform.position.x, TopHeight));
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.transform.localPosition.y > transform.localPosition.y)
			{
				Descend();
				descending = true;
				if (runtimeGameData.Energy == 1f)
				{
					blinkObject.StopBlinking();
				}
				else
				{
					blinkObject.StartBlinking();
				}
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			descending = false;
			blinkObject.StopBlinking();
		}

		private void Descend()
		{
			var change = Vector2.down * descendingSpeed * Time.deltaTime;
			if (controls.Player.ElevatorDown.ReadValue<float>() != 0)
				change *= descendBoostMultiplier;
			var finalPos = new Vector2(rigidbody.position.x, Mathf.Clamp(rigidbody.position.y + change.y, BottomHeight, TopHeight));
			rigidbody.MovePosition(finalPos);
		}

		private void Ascend()
		{
			var change = Vector2.up * ascendingSpeed * Time.deltaTime;
			if (runtimeGameData.ShieldActive)
				change *= 1.5f;
			rigidbody.MovePosition(new Vector3(rigidbody.position.x, Mathf.Clamp(rigidbody.position.y + change.y, BottomHeight, TopHeight)));
		}
	}
}

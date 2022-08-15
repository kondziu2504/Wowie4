using UnityEngine;

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

		private float originalHeight;

		private float BottomHeight => Application.isPlaying ? originalHeight : transform.position.y;
		private float TopHeight => BottomHeight + maxHeight;

		private bool descending = false;

		protected Controls controls;

		private new Rigidbody2D rigidbody;

		private void Awake()
		{
			originalHeight = transform.position.y;
			rigidbody = GetComponent<Rigidbody2D>();
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
			runtimeGameData.Energy = Mathf.InverseLerp(TopHeight, BottomHeight, transform.position.y);
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
			if (collision.transform.position.y > transform.position.y)
			{
				Descend();
				descending = true;
				blinkObject.StartBlinking();
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

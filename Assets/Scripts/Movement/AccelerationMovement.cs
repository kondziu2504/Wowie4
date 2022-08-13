using UnityEngine;

namespace Onion2D.Movement
{
	public class AccelerationMovement : MonoBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float acceleration, decceleration, maxSpeed;
		public float MaxSpeed => maxSpeed;
		float horizontalSpeed;
		public float HorizontalSpeed => horizontalSpeed;

		public float Control { get; set; } = 1f;

		private void Update()
		{
			body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, horizontalSpeed, Mathf.Max(decceleration, acceleration) * Time.deltaTime * Control), body.velocity.y);
		}
		public void Move(float input)
		{
			if (!Mathf.Approximately(input, 0f))
			{
				Accelerate(input);
			}
			else
			{
				Deccelerate();
			}
		}

		public void ResetSpeed()
        {
			horizontalSpeed = 0f;
        }

		private void Accelerate(float input)
		{
			if (Mathf.Sign(input) != Mathf.Sign(horizontalSpeed))
			{
				horizontalSpeed = 0;
			}
			horizontalSpeed += input * acceleration * Time.deltaTime;
			horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);
		}
		private void Deccelerate()
		{
			horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, decceleration * Time.deltaTime);
		}

	}
}

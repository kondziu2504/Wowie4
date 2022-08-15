using UnityEngine;

namespace Onion2D.Movement
{
	public class JumpController : MonoBehaviour
	{
		[SerializeField] Rigidbody2D body;
		[SerializeField] float jumpForce;
		[SerializeField] AudioSource jump;
		public void PerformeJump()
		{
			jump.Play();
			body.AddForce(Vector2.up * jumpForce * Mathf.Sign(body.gravityScale), ForceMode2D.Impulse);
		}

	}
}

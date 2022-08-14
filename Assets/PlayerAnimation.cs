using Onion2D.Movement;
using UnityEngine;

namespace Wowie4
{
	public class PlayerAnimation : InputBehaviour
	{
		[SerializeField] Animator animator;
		[SerializeField] GroundCheck groundCheck;
		[SerializeField] Rigidbody2D body;

		bool isGroundedLastFrame;
		private void Update()
		{
			bool isGrounded = groundCheck.IsGrounded;
			animator.SetBool("IsGrounded", isGrounded);
			float movement = controls.Player.Movement.ReadValue<float>();
			animator.SetFloat("Velocity", Mathf.Abs(movement));
			if (isGroundedLastFrame == false && isGrounded)
			{
				animator.SetTrigger("Land");
			}
			isGroundedLastFrame = isGrounded;

			if (movement > 0)
			{
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			if (movement < 0)
			{
				transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
		}
	}
}

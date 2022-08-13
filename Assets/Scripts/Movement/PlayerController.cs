using Onion2D.Movement;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputBehaviour
{
	[SerializeField] Rigidbody2D body;
	[SerializeField] AccelerationMovement accelerationMovement;
	[SerializeField] JumpController jumpController;
	[SerializeField] GroundCheck groundCheck;
	[SerializeField][Range(0, 1)] float jumpCut;
	[SerializeField] float coyoteTime = .15f;
	[SerializeField] float jumpBufferTime = .15f;

	float lastTimeOnGround = -1;
	float lastJumpInput = -1;
	private void Update()
	{
		if (enabledInputs)
		{
			float input = controls.Player.Movement.ReadValue<float>();
			accelerationMovement.Move(input);
		}

		UpdateLastTimeOnGround();
	}

	bool enabledInputs = true;
	public bool EnabledInputs
	{
		get => enabledInputs;
		set
		{
			if (value == true)
				SubscribeInputEvents();
			else
				UnsubscribeInputEvents();
			enabledInputs = value;
			accelerationMovement.enabled = enabledInputs;
		}
	}

	protected override void SubscribeInputEvents()
	{
		controls.Player.Jump.performed += Jump;
		controls.Player.Jump.canceled += JumpCanceled;
	}


	protected override void UnsubscribeInputEvents()
	{
		controls.Player.Jump.performed -= Jump;
		controls.Player.Jump.canceled -= JumpCanceled;
	}
	private void JumpCanceled(InputAction.CallbackContext obj)
	{
		if (Mathf.Sign(body.velocity.y) == Mathf.Sign(body.gravityScale))
		{
			CutVelocityY();
		}
	}
	private void CutVelocityY() => body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpCut);
	private void Jump(InputAction.CallbackContext obj)
	{

		if (groundCheck.IsGrounded || CanJumpWithoutGround())
		{
			jumpController.PerformeJump();
			lastTimeOnGround -= coyoteTime;
		}
		else
		{
			lastJumpInput = Time.time;
		}
	}
	private bool CanJumpWithoutGround() => Time.time - lastTimeOnGround < coyoteTime;
	private void UpdateLastTimeOnGround()
	{
		if (groundCheck.IsGrounded)
		{
			lastTimeOnGround = Time.time;
			JumpBuffer();
		}
	}
	private void JumpBuffer()
	{
		if (Time.time - lastJumpInput < jumpBufferTime)
		{
			lastJumpInput -= jumpBufferTime;
			jumpController.PerformeJump();

			if (!controls.Player.Jump.IsPressed())
			{
				CutVelocityY();
			}
		}
	}
}

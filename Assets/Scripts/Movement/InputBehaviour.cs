using UnityEngine;

public abstract class InputBehaviour : MonoBehaviour
{
	protected Controls controls;
	protected virtual void OnEnable()
	{
		controls = new Controls();
		controls.Enable();
		SubscribeInputEvents();
	}
	protected virtual void OnDisable()
	{
		UnsubscribeInputEvents();
		controls.Disable();
	}

	protected virtual void SubscribeInputEvents() { }
	protected virtual void UnsubscribeInputEvents() { }
}

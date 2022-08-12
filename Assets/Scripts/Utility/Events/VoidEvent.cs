using System;
using UnityEngine;

namespace Utility.Events
{
	[CreateAssetMenu(menuName = "SO/Events/Void")]
	public class VoidEvent : ScriptableObject
	{
		public event Action OnEventRaised;
		public void RaiseEvent()
		{
			OnEventRaised?.Invoke();
		}
	}
}

using System;
using UnityEngine;


namespace Utility.SOVariables
{
	public class SOVariable<T> : ScriptableObject
	{
		[SerializeField] T initialValue;
		public T RuntimeValue
		{
			get => runtimeValue;
			set
			{
				runtimeValue = value;
				if (value is not null)
				{
					OnValueLoaded?.Invoke();
				}
			}
		}
		private T runtimeValue;
		public event Action OnValueLoaded;
		private void OnEnable()
		{
			RuntimeValue = initialValue;
		}
	}

}

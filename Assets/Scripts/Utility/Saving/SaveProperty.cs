using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Utility.Saving
{
    public abstract class SaveProperty<T> where T : struct
    {
        protected string key;
        protected T defaultValue;

        public SaveProperty(string key, [MaybeNull] T defaultValue = default(T))
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

		public abstract T Get();
		public void Save(T newValue)
		{
			SaveMethod(newValue);
			PlayerPrefs.Save();
		}
		protected abstract void SaveMethod(T newValue);
	}

	public class FloatProperty : SaveProperty<float>
	{
		public FloatProperty(string key, float defaultValue) : base(key, defaultValue) { }
		public override float Get() => PlayerPrefs.GetFloat(key, defaultValue);
		protected override void SaveMethod(float newValue) => PlayerPrefs.SetFloat(key, newValue);
	}
}

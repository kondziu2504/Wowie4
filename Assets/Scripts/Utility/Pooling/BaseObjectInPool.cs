using UnityEngine;
using UnityEngine.Pool;

namespace Utility.Pooling
{
	public class BaseObjectInPool : MonoBehaviour
	{
		private ObjectPool<BaseObjectInPool> pool;
		public void Release() => pool.Release(this);
		public void OnCreate(ObjectPool<BaseObjectInPool> pool) => this.pool = pool;
		public virtual void OnGet()
		{
			gameObject.hideFlags = HideFlags.None;
			gameObject.SetActive(true);
		}
		public virtual void OnRelease()
		{
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			gameObject.SetActive(false);
		}
		public virtual void OnDestroyed() => gameObject.SetActive(false);
	}


}

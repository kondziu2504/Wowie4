using UnityEngine;
using UnityEngine.Pool;

namespace Utility.Pooling
{
	[CreateAssetMenu(menuName = "SO/ScriptablePool")]
	public class ScriptablePool : ScriptableObject
	{
		[SerializeField] BaseObjectInPool prefab;
		[SerializeField] int defaultCapacity = 10;
		[SerializeField] int maxSize = 10000;
		public ObjectPool<BaseObjectInPool> Pool { get; private set; }


		private void OnEnable()
		{
			Pool = new ObjectPool<BaseObjectInPool>(Create, Get, Release, DestroyObjectInPool, defaultCapacity: defaultCapacity, maxSize: maxSize);
		}

		private void DestroyObjectInPool(BaseObjectInPool obj)
		{
			obj.OnDestroyed();
		}

		private void Release(BaseObjectInPool obj)
		{
			obj.OnRelease();
		}

		private void Get(BaseObjectInPool obj)
		{
			obj.OnGet();
		}

		private BaseObjectInPool Create()
		{
			BaseObjectInPool objectInPool = Instantiate(prefab);
			objectInPool.OnCreate(Pool);
			return objectInPool;
		}
	}


}

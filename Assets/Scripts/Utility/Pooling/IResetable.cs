using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Pooling
{
    public interface IResetable
    {
        public void ResetState();
    }

    [System.Serializable]
    public class IResetableContainer : IUnifiedContainer<IResetable> { }
}

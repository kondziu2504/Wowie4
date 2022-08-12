using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Pooling
{
    public class ResetablePoolObject : BaseObjectInPool
    {
        [SerializeField] IResetableContainer resetable;

        public override void OnGet()
        {
            base.OnGet();
            resetable.Result.ResetState();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utility.Events.Editor
{
    public abstract class SimpleEventEditor<EvntT, ArgT> : BaseEventEditor<EvntT, BaseField<ArgT>, ArgT, ArgT>
        where EvntT : BaseEvent<ArgT>
    {
        protected override ArgT GetFieldValue(BaseField<ArgT> field) => field.value;
    }
}

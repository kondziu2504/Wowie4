using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Utility.Events;

namespace Utility.Events.Editor
{
    [CustomEditor(typeof(IntEvent), true)]
    public class IntEventEditor : SimpleEventEditor<IntEvent, int>
    {
        protected override BaseField<int> CreateField() => new IntegerField("Argument");
    }
}

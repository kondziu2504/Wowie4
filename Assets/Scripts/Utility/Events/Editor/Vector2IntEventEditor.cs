using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Utility.Events;

namespace Utility.Events.Editor
{
    [CustomEditor(typeof(Vector2IntEvent), true)]
    public class Vector2IntEventEditor : SimpleEventEditor<Vector2IntEvent, Vector2Int>
    {
        protected override BaseField<Vector2Int> CreateField() => new Vector2IntField("Argument");
    }

}


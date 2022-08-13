using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utility.Events.Editor;

namespace Wowie4
{
    [CustomEditor(typeof(ActionChosenEvent))]
    public class ActionChosenEventEditor : BaseEventEditor<ActionChosenEvent, EnumField, Enum, Action.Type>
    {
        protected override EnumField CreateField()
        {
            var defaultValue = (Action.Type)Enum.GetValues(typeof(Action.Type)).GetValue(0);
            return new EnumField(defaultValue);
        }

        protected override Action.Type GetFieldValue(EnumField field) => Enum.Parse<Action.Type>(field.text);
    }
}

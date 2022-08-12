using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Utility.Events;

namespace Utility.Editor
{
    [CustomEditor(typeof(VoidEvent), true)]
    public class VoidEventEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VoidEvent evnt = (VoidEvent)target;

            VisualElement container = new();
            InspectorElement.FillDefaultInspector(container, serializedObject, this);

            var raiseBtn = Utility.CreateRaiseEventBtn(evnt.RaiseEvent);

            container.Add(raiseBtn);

            return container;
        }
    }
}

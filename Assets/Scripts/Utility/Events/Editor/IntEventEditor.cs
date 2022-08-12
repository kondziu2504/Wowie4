using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Utility.Events;

namespace Utility.Editor
{
    [CustomEditor(typeof(IntEvent))]
    public class IntEventEditor : UnityEditor.Editor
    {
        IntEvent evnt => (IntEvent)target;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement container = new();

            InspectorElement.FillDefaultInspector(container, serializedObject, this);

            var debugLabel = new Label("Debug");
            debugLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

            container.Add(debugLabel);
            container.Add(CreateRaiseEventElement());

            return container;
        }

        private VisualElement CreateRaiseEventElement()
        {
            VisualElement raiseEventContainer = new();
            raiseEventContainer.style.flexDirection = FlexDirection.Row;

            var argField = new IntegerField("Argument");
            argField.style.flexGrow = new StyleFloat(1f);

            var raiseEventBtn = Utility.CreateRaiseEventBtn(() => evnt.RaiseEvent(argField.value));

            raiseEventContainer.Add(argField);
            raiseEventContainer.Add(raiseEventBtn);

            return raiseEventContainer;
        }
    }
}

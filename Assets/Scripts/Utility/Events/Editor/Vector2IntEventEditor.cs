using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Utility.Editor;
using Utility.Events;

[CustomEditor(typeof(Vector2IntEvent))]
public class Vector2IntEventEditor : Editor
{
    Vector2IntEvent evnt => (Vector2IntEvent)target;

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

        var argField = new Vector2IntField("Argument");
        argField.style.flexGrow = new StyleFloat(1f);

        var raiseEventBtn = Utility.Editor.Utility.CreateRaiseEventBtn(() => evnt.RaiseEvent(argField.value));

        raiseEventContainer.Add(argField);
        raiseEventContainer.Add(raiseEventBtn);

        return raiseEventContainer;
    }
}

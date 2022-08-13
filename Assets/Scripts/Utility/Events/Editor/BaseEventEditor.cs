using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utility.Events.Editor
{
    public abstract class BaseEventEditor<EvntT, FieldT, FieldArgT, ArgT> : UnityEditor.Editor 
        where EvntT : BaseEvent<ArgT>
        where FieldT : BaseField<FieldArgT>
    {
        EvntT evnt => (EvntT)target;

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

            var argField = CreateField();
            argField.style.flexGrow = new StyleFloat(1f);

            var raiseEventBtn = Utility.CreateRaiseEventBtn(() => evnt.RaiseEvent(GetFieldValue(argField)));

            raiseEventContainer.Add(argField);
            raiseEventContainer.Add(raiseEventBtn);

            return raiseEventContainer;
        }

        protected abstract FieldT CreateField();
        protected abstract ArgT GetFieldValue(FieldT field);
    }
}

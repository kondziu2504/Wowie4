using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utility.Editor
{
    public static class Utility
    {
        public static VisualElement CreateRaiseEventBtn(System.Action action)
        {
            var raiseEventBtn = new Button(action);
            raiseEventBtn.text = "Raise";
            raiseEventBtn.SetEnabled(Application.isPlaying);
            raiseEventBtn.tooltip = "Only possible in play mode";
            raiseEventBtn.style.flexGrow = new StyleFloat(1f);

            return raiseEventBtn;
        }
    }
}

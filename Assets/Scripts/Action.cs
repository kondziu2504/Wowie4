using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wowie4
{
    public class Action
    {
        public enum Type { Red, Green, Blue }

        public static Color GetActionColor(Type type)
        {
            return type switch
            {
                Type.Red => Color.red,
                Type.Green => Color.green,
                Type.Blue => Color.blue,
                _ => throw new NotImplementedException("Action color not assigned")
            };
        }
    }
}


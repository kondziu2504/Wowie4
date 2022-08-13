using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Events;

namespace Wowie4
{
    [CreateAssetMenu(menuName = "SO/Events/ActionChosenEvent")]
    public class ActionChosenEvent : BaseEvent<Action.Type> { }

}

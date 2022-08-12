using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Utility.Modifiers
{

    [System.Serializable]
    public class ModifiersSet
    {
        [SerializeField] List<Modifier> flatModifiers;
        [SerializeField] List<Modifier> percentModifiers;

        public ICollection<Modifier> FlatModifiers => flatModifiers;
        public ICollection<Modifier> PercentModifiers => percentModifiers;

        public float AppliedTo(float baseValue)
        {
            float totalPercentModifier = PercentModifiers.Aggregate(1f, (currVal, modifier) => currVal * modifier.Value);
            float totalFlatModifier = FlatModifiers.Sum(modifier => modifier.Value);
            return baseValue * totalPercentModifier + totalFlatModifier;
        }
    }
}

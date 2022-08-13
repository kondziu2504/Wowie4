using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
    [CreateAssetMenu(menuName = "SO/RuntimeGameData")]
    public class RuntimeGameData : ScriptableObject
    {
        public List<Bullet> Bullets { get; } = new List<Bullet>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
    [CreateAssetMenu(menuName = "SO/RuntimeGameData")]
    public class RuntimeGameData : ScriptableObject
    {
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        [SerializeField, Range(0, 1f)] private float mouthOpenAmount = 0f;
        public float MouthOpenAmount
        {
            get => mouthOpenAmount;
            set => mouthOpenAmount = Mathf.Clamp01(value);
        }
    }

}

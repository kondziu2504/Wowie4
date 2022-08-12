using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class RandomUtility
    {
        public static int GetRandomIndex(float[] weights)
        {
            float totalWeights = weights.Sum();
            float randVal = Random.Range(0f, totalWeights);
            float currentWeight = 0;
            for(int i = 0; i < weights.Length; i++)
            {
                currentWeight += weights[i];
                if (randVal < currentWeight)
                    return i;
            }

            throw new System.InvalidOperationException();
        }

        public static Vector2 GetRandomPointInsideRect(Rect rect)
        {
            return new Vector2(
                Random.Range(rect.xMin, rect.xMax),
                Random.Range(rect.yMin, rect.yMax)
            );
        }

        public static T RandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];

        public static T RandomElement<T>(this IList<T> list) => list[Random.Range(0, list.Count)];

    }
}

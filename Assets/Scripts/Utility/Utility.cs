using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class Utility
    {
        public static Rect GetCameraRect(Camera camera)
        {
            float camHeight = camera.orthographicSize * 2f;
            float camWidth = camHeight * camera.aspect;

            return new Rect(
                camera.transform.position - new Vector3(camWidth / 2f, camHeight / 2f),
                new Vector2(camWidth, camHeight)
            );
        }

        public static Rect ScaledRelativeToCenter(this Rect rect, float scale)
        {
            Vector2 sizeDiff = rect.size * (1f - scale);
            rect.size = rect.size * scale;
            rect.position = rect.position + sizeDiff / 2f;
            return rect;
        }

        public static Vector2[] Corners(this Rect rect, bool relative = false)
        {
            Vector2[] corners = new Vector2[]
            {
            new Vector2(rect.xMin, rect.yMin),
            new Vector2(rect.xMin, rect.yMax),
            new Vector2(rect.xMax, rect.yMax),
            new Vector2(rect.xMax, rect.yMin)
            };

            if (relative)
                for (int i = 0; i < 4; i++)
                    corners[i] -= rect.position;

            return corners;
        }
        public static T[] Shuffle<T>(this T[] array)
        {
            System.Random rnd = new System.Random();
            return array.OrderBy(x => rnd.Next()).ToArray();
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            System.Random rnd = new System.Random();
            return list.OrderBy(x => rnd.Next()).ToList();
        }
    }
}

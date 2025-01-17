﻿using UnityEngine;

namespace MUtility
{
    public static class DrawableHelper
    {
        public static Drawable ToDrawable(this Rect rectangle)
        {
            float halfX = rectangle.size.x / 2f;
            float halfY = rectangle.size.y / 2f;
            float cX = rectangle.center.x;
            float cY = rectangle.center.y;

            var _00 = new UnityEngine.Vector3(cX - halfX, cY - halfY, 0);
            var _01 = new UnityEngine.Vector3(cX - halfX, cY + halfY, 0);
            var _10 = new UnityEngine.Vector3(cX + halfX, cY - halfY, 0);
            var _11 = new UnityEngine.Vector3(cX + halfX, cY + halfY, 0);

            return new Drawable(new[] { _00, _01, _11, _10, _00 });
        }

        public static Drawable ToDrawable(this Bounds bounds)
        {
            float halfX = bounds.size.x / 2f;
            float halfY = bounds.size.y / 2f;
            float halfZ = bounds.size.z / 2f;
            float cX = bounds.center.x;
            float cY = bounds.center.y;
            float cZ = bounds.center.z;

            var _000 = new UnityEngine.Vector3(cX - halfX, cY - halfY, cZ - halfZ);
            var _001 = new UnityEngine.Vector3(cX - halfX, cY - halfY, cZ + halfZ);
            var _010 = new UnityEngine.Vector3(cX - halfX, cY + halfY, cZ - halfZ);
            var _011 = new UnityEngine.Vector3(cX - halfX, cY + halfY, cZ + halfZ);
            var _100 = new UnityEngine.Vector3(cX + halfX, cY - halfY, cZ - halfZ);
            var _101 = new UnityEngine.Vector3(cX + halfX, cY - halfY, cZ + halfZ);
            var _110 = new UnityEngine.Vector3(cX + halfX, cY + halfY, cZ - halfZ);
            var _111 = new UnityEngine.Vector3(cX + halfX, cY + halfY, cZ + halfZ);
            return new Drawable
            (
               new[] { _000, _010, _110, _100, _000 },
               new[] { _001, _011, _111, _101, _001 },
               new[] { _000, _001 },
               new[] { _010, _011 },
               new[] { _100, _101 },
               new[] { _110, _111 }
            );
        }

        const float defaultCrossRadius = 0.5f;
        public static Drawable ToDrawable(this UnityEngine.Vector3 position) => position.ToDrawable(defaultCrossRadius);

        public static Drawable ToDrawable(this UnityEngine.Vector3 position, float crossRadius)
        {

            float c = 1 / Mathf.Sqrt(2f);
            UnityEngine.Vector3 _00 = position + new UnityEngine.Vector3(-c * crossRadius, -c * crossRadius);
            UnityEngine.Vector3 _01 = position + new UnityEngine.Vector3(-c * crossRadius, c * crossRadius);
            UnityEngine.Vector3 _10 = position + new UnityEngine.Vector3(c * crossRadius, -c * crossRadius);
            UnityEngine.Vector3 _11 = position + new UnityEngine.Vector3(c * crossRadius, c * crossRadius);

            return new Drawable
            (
               new[] { _00, _11 },
               new[] { _01, _10 }
            );
        }
    }
}

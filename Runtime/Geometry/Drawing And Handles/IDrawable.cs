﻿using UnityEngine;

namespace MUtility
{
    public interface IDrawable
    {
        Drawable ToDrawable();
    }
    
    public static class DrawableExtensions
    {
        public static void DrawGizmo(this IDrawable self, Color color) => self.ToDrawable().DrawGizmo(color);
        public static void DrawDebug(this IDrawable self, Color color) => self.ToDrawable().DrawDebug(color);

        public static void DrawGizmo(this IDrawable self, Transform transform, Color color) => self.ToDrawable().DrawGizmo(transform, color);
        public static void DrawDebug(this IDrawable self, Transform transform, Color color) => self.ToDrawable().DrawDebug(transform, color);
        
        public static void DrawGizmo(this IDrawable self, Transform transform, Space space, Color color) => self.ToDrawable().DrawGizmo(transform, space, color);
        public static void DrawDebug(this IDrawable self, Transform transform, Space space, Color color) => self.ToDrawable().DrawDebug(transform, space, color);

        public static void DrawGizmo(this IDrawable self, UnityEngine.Vector3 offet, Color color) => self.ToDrawable().DrawGizmo(offet, color);
        public static void DrawDebug(this IDrawable self, UnityEngine.Vector3 offet, Color color) => self.ToDrawable().DrawDebug(offet, color);

        public static void DrawGizmo(this IDrawable self, Quaternion rotate, Color color) => self.ToDrawable().DrawGizmo(rotate, color);
        public static void DrawDebug(this IDrawable self, Quaternion rotate, Color color) => self.ToDrawable().DrawDebug(rotate, color);

        public static void DrawGizmo(this IDrawable self, UnityEngine.Vector3 offset, Quaternion rotate, float scale, Color color) =>
            self.ToDrawable().DrawGizmo(offset, rotate, scale, color);
        public static void DrawDebug(this IDrawable self, UnityEngine.Vector3 offset, Quaternion rotate, float scale, Color color) =>
            self.ToDrawable().DrawDebug(offset, rotate, scale, color);

        public static void DrawGizmo(this IDrawable self, UnityEngine.Vector3 offset, Quaternion rotate, UnityEngine.Vector3 scale, Color color) =>
            self.ToDrawable().DrawGizmo(offset, rotate, scale, color);
        public static void DrawDebug(this IDrawable self, UnityEngine.Vector3 offset, Quaternion rotate, UnityEngine.Vector3 scale, Color color) =>
            self.ToDrawable().DrawDebug(offset, rotate, scale, color);
    }
    
}                                  
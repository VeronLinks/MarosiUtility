﻿// ---------------------------------------------------------------------------- 
// Author: Anton
// https://github.com/antontidev
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

#if UNITY_EDITOR 
	using UnityEditor;
#endif 

namespace MUtility
{
    /// <summary>
    /// Used to pick scene from inspector.
    /// Consider to use <see cref="SceneReference"/> type instead as it is more flexible
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SceneAttribute : PropertyAttribute
    {
    } 

#if UNITY_EDITOR 
    [CustomPropertyDrawer(typeof(SceneAttribute))]
	public class SceneDrawer : PropertyDrawer
	{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (property.propertyType == SerializedPropertyType.String)
            {
                var sceneObject = GetSceneObject(property.stringValue);
                var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
                if (scene == null)
                {
                    property.stringValue = "";
                }
                else if (scene.name != property.stringValue)
                {
                    var sceneObj = GetSceneObject(scene.name);
                    if (sceneObj == null)
                    {
                        Debug.LogWarning("The scene " + scene.name + " cannot be used. To use this scene add it to the build settings for the project");
                    }
                    else
                    {
                        property.stringValue = scene.name;
                    }
                }
            }
            else
                EditorGUI.LabelField(position, label.text, "Use [Scene] with strings.");
        }
        protected SceneAsset GetSceneObject(string sceneObjectName)
        {
            if (string.IsNullOrEmpty(sceneObjectName))
            {
                return null;
            }

            foreach (var editorScene in EditorBuildSettings.scenes)
            {
                if (editorScene.path.IndexOf(sceneObjectName) != -1)
                {
                    return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
                }
            }
            Debug.LogWarning("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
            return null;
        }
    }
#endif
}
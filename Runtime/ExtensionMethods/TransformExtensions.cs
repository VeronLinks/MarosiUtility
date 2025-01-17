﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MUtility
{
public static class TransformExtensions
{
	static UnityEngine.Vector3 _cacheVector3;

	public static void DestroyChildren(this Transform transform, params Transform[] exceptions)
	{
		if (Application.isPlaying)
			DestroyChildren_NormalDestroy(transform, exceptions);
		else
			DestroyChildren_DestroyImmediate(transform, exceptions);
	}

	public static void DestroyChildren_NormalDestroy(this Transform transform, params Transform[] exceptions)
	{
		int childCount = transform.childCount;
		for (int i = childCount - 1; i >= 0; i--)
		{
			Transform child = transform.GetChild(i);
			if (exceptions.Contains(child))
			{
				continue;
			}
			Object.Destroy(child.gameObject);
		}
	}

	public static void DestroyChildren_DestroyImmediate(this Transform transform,  params Transform[] exceptions)
	{
		int childCount = transform.childCount;
		for (int i = childCount - 1; i >= 0; i--)
		{
			Transform child = transform.GetChild(i);
			if (exceptions.Contains(child))
			{
				continue;
			}
			try
			{
				Object.DestroyImmediate(child.gameObject);
			}
			catch (InvalidOperationException) { }
		}
	}

	public static bool DestroyChild(this Transform transform, string name)
	{
		Transform child = transform.Find(name);
		if (child != null)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(child.gameObject);
			}
			else
			{
				Object.DestroyImmediate(child.gameObject);
			}
			return true;
		}
		return false;
	}

	public static void SetParentAndAlignToDefault(this Transform self, Transform parent)
	{
		self.SetParent(parent);
		self.localScale = UnityEngine.Vector3.one;
		self.localRotation = Quaternion.identity;
		self.localPosition = UnityEngine.Vector3.zero;
		var rect = self.GetComponent<RectTransform>();
		if (rect == null) return;
		rect.sizeDelta = new Vector2(0, 0);
		rect.anchoredPosition = new Vector2(0, 0);
	}

	public static string[] GetChildrenNames(this Transform self)
	{
		var result = new string[self.childCount];
		var i = 0;
		foreach (Transform t in self)
		{
			result[i] = t.name;
			i++;
		}
		return result;
	}
    
	public static Transform CreateChild(this Transform self, string childName)
	{
		var go = new GameObject(childName);
		Transform t = go.transform;
		t.SetParentAndAlignToDefault(self);
		return t;
	}

	public static void DestroyAllComponents(this Transform self)
	{
		Component[] components = self.GetComponents<Component>();
		foreach (Component t in components)
			Object.Destroy(t);
	}


	public static void SetPositionX(this Transform self, float value)
	{
		_cacheVector3 = self.position;
		_cacheVector3.x = value;
		self.position = _cacheVector3;
	}
	public static void SetPositionY(this Transform self, float value)
	{
		_cacheVector3 = self.position;
		_cacheVector3.y = value;
		self.position = _cacheVector3;
	}
	public static void SetPositionZ(this Transform self, float value)
	{
		_cacheVector3 = self.position;
		_cacheVector3.z = value;
		self.position = _cacheVector3;
	}
	public static void SetLocalPositionX(this Transform self, float value)
	{
		_cacheVector3 = self.localPosition;
		_cacheVector3.x = value;
		self.localPosition = _cacheVector3;
	}
	public static void SetLocalPositionY(this Transform self, float value)
	{
		_cacheVector3 = self.localPosition;
		_cacheVector3.y = value;
		self.localPosition = _cacheVector3;
	}
	public static void SetLocalPositionZ(this Transform self, float value)
	{
		_cacheVector3 = self.localPosition;
		_cacheVector3.z = value;
		self.localPosition = _cacheVector3;
	}
	public static void SetLocalScaleX(this Transform self, float value)
	{
		_cacheVector3 = self.localScale;
		_cacheVector3.x = value;
		self.localScale = _cacheVector3;
	}

	public static void SetLocalScaleY(this Transform self, float value)
	{
		_cacheVector3 = self.localScale;
		_cacheVector3.y = value;
		self.localScale = _cacheVector3;
	}

	public static void SetLocalScale(this Transform transform, float scale)
	{
		transform.localScale = new UnityEngine.Vector3(scale, scale, scale);
	}
	
	public static void SetLocalScaleZ(this Transform self, float value)
	{
		_cacheVector3 = self.localScale;
		_cacheVector3.z = value;
		self.localScale = _cacheVector3;
	}

	public static void SetGlobalScale(this Transform transform, UnityEngine.Vector3 globalScale)
	{
		transform.localScale = UnityEngine.Vector3.one;
		UnityEngine.Vector3 lossyScale = transform.lossyScale;
		transform.localScale = new UnityEngine.Vector3(
			globalScale.x / lossyScale.x, 
			globalScale.y / lossyScale.y,
			globalScale.z / lossyScale.z);
	}

	public static void SetGlobalScale(this Transform transform, float globalScale)
	{
		transform.localScale = UnityEngine.Vector3.one;
		UnityEngine.Vector3 lossyScale = transform.lossyScale;
		transform.localScale = new UnityEngine.Vector3(
			globalScale / lossyScale.x,
			globalScale / lossyScale.y, 
			globalScale / lossyScale.z);
	}
	
	public static float GetMaxGlobalScaleDimension(this Transform transform)
	{
		UnityEngine.Vector3 scale = transform.lossyScale;
		return Mathf.Max(scale.x, scale.y, scale.z);
	}

	public static UnityEngine.Vector3 TransformPointUnscaled(this Transform transform, UnityEngine.Vector3 position)
	{
		Matrix4x4 localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, UnityEngine.Vector3.one);
		return localToWorldMatrix.MultiplyPoint3x4(position);
	}

	public static UnityEngine.Vector3 InverseTransformPointUnscaled(this Transform transform, UnityEngine.Vector3 position)
	{
		Matrix4x4 worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, UnityEngine.Vector3.one).inverse;
		return worldToLocalMatrix.MultiplyPoint3x4(position);
	}

	public static Quaternion TransformRotation(
		this Transform transform, Quaternion rotation)
	{
		UnityEngine.Vector3 forward = transform.TransformDirection(rotation * UnityEngine.Vector3.forward);
		UnityEngine.Vector3 up = transform.TransformDirection(rotation * UnityEngine.Vector3.up);
		return Quaternion.LookRotation(forward, up);
	}
	
	public static Quaternion InverseTransformRotation(
		this Transform transform, Quaternion rotation)
	{
		UnityEngine.Vector3 forward = transform.InverseTransformDirection(rotation * UnityEngine.Vector3.forward);
		UnityEngine.Vector3 up = transform.InverseTransformDirection(rotation * UnityEngine.Vector3.up);
		return Quaternion.LookRotation(forward, up);
	}


	public static IEnumerable<Transform> AllChildrenRecursively(this Transform transform)
	{ 
		for (var i = 0; i < transform.childCount; i++)
			foreach (Transform child in SelfAndAllChildrenRecursively(transform.GetChild(i)))
				yield return child;
	}

	public static IEnumerable<Transform> SelfAndAllChildrenRecursively(this Transform transform)
	{
		yield return transform; 
		for (var i = 0; i< transform.childCount ; i++ )
			foreach (Transform child in SelfAndAllChildrenRecursively(transform.GetChild(i)))
				yield return child;
	}
    
	public static Transform FindChildRecursive(this Transform parent, string name)
	{
		foreach (Transform child in parent)
		{
			if (child.name.Contains(name))
				return child;

			Transform result = child.FindChildRecursive(name);
			if (result != null)
				return result;
		}
		return null;
	}
    
	public static IEnumerable<TComponent> EnumerateComponentsInParents<TComponent>(
		this Transform self, 
		bool includeInactive = false)
	{
		if(self == null) yield break;
		if(!includeInactive && !self.gameObject.activeInHierarchy) yield break;
	    
		foreach (TComponent component in self.GetComponents<TComponent>())
			yield return component;
		foreach (TComponent component in EnumerateComponentsInParents<TComponent>(self.parent))
			yield return component;
	}
    
	public static IEnumerable<Component> EnumerateComponentsInParents(
		this Transform self,
		Type componentType,
		bool includeInactive = false)
	{
		if(self == null) yield break;
		if(!includeInactive && !self.gameObject.activeInHierarchy) yield break;
	    
		foreach (Component component in self.GetComponents(componentType))
			yield return component;
		foreach (Component component in EnumerateComponentsInParents(self.parent, componentType))
			yield return component; 
	}

	public static T GetTransformInParentsOnly<T>(this Transform self) where T : Component
	{
		while (true)
		{
			if (self.parent == null) return null;
			var comp = self.parent.GetComponent<T>();
			if (comp != null) return comp;
			self = self.parent;
		}
	}

	public static Object[] GetAllUnityObjects(this Transform root)
	{
		var allObjects = new List<Object>( );
		foreach (Transform transform in root.SelfAndAllChildrenRecursively())
		{
			allObjects.Add(transform.gameObject);
			allObjects.AddRange(transform.GetComponents<Component>());
            
		}
		return allObjects.ToArray();
	}
	
	
	public static Pose GetPose(this Transform transform) => new Pose(transform.position, transform.rotation);

	public static Pose TransformPose(this Transform transform, Pose localPose) => new Pose
	{
		position = transform.TransformPoint(localPose.position),
		rotation = transform.TransformRotation(localPose.rotation)
	};

	public static Pose InverseTransformPose(this Transform transform, Pose localPose) => new Pose
	{
		position = transform.InverseTransformPoint(localPose.position),
		rotation = transform.InverseTransformRotation(localPose.rotation)
	};

	public static void SetPose(this Transform transform, Pose pose) =>
		transform.SetPositionAndRotation(pose.position, pose.rotation);

	public static void SetLocalPose(this Transform transform, Pose pose)
	{
		transform.localPosition = pose.position;
		transform.localRotation = pose.rotation;
	}

}
}
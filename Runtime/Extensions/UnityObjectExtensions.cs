using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Extension methods related to UnityObjects and/or derived types thereof.
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class UnityObjectExtensions
    {
        public static string NameAndID(this UnityEngine.Object obj)
        {
            return $"{obj.name}:{obj.GetInstanceID().ToString()}";
        }

        public static T[] FindByType<T>(FindObjectsSortMode sortMode = FindObjectsSortMode.None) where T : UnityEngine.Object
        {
            return GameObject.FindObjectsByType<T>(sortMode);
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class GameObjectExtensions
    {
        public static IEnumerable<GameObject> Children(this GameObject obj, Predicate<Transform> filter = null)
        {
            return obj.transform.Children(filter ?? (_ => true)).Select(t => t.gameObject);
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class TransformExtensions
    {
        public static string Path(this Transform t)
        {
            return t.parent == null
                ? t.name
                : $"{t.parent.Path()}/{t.name}";
        }

        public static IEnumerable<Transform> Children(this Transform t, Predicate<Transform> filter = null)
        {
            filter ??= _ => true;

            Transform[] children = new Transform[t.childCount];
            for (int i = 0; i < t.childCount; i++)
                children[i] = t.GetChild(i);

            return children.Where(t => filter(t));
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class ComponentExtensions
    {
        public static string Path(this Component component)
        {
            return component.transform.Path() + "/" + component.GetType().ToString();
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}
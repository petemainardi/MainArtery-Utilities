using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace MainArtery.Utilities.Unity.Editor
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Extension methods for the SerializedProperty class.
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class SerializedPropertyExtensions
    {
        // Code courtesy of:
        // http://sketchyventures.com/2015/08/07/unity-tip-getting-the-actual-object-from-a-custom-property-drawer/
        public static T GetTypedObject<T>(this SerializedProperty prop, FieldInfo info) where T : class
        {
            var obj = info.GetValue(prop.serializedObject.targetObject);

            return obj == null
                ? null
                : obj.GetType().IsArray
                ? ((T[])obj)[Convert.ToInt32(new string(prop.propertyPath.Where(c => char.IsDigit(c)).ToArray()))]
                : obj as T;
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}

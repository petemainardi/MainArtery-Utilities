using UnityEngine;
using UnityEditor;
using MainArtery.Utilities.Unity.Attributes;

// Code courtesy of:
// https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/

namespace MainArtery.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool previousGuiState = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = previousGuiState;
        }
    }
}
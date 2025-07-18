#if ODIN_INSPECTOR
using System;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using MainArtery.Utilities.Unity.Attributes;

namespace MainArtery.Utilities.Unity.Editor
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Draw a horizontal dividing line before or after the decorated item.
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public class SeparatorAttributeDrawer : OdinAttributeDrawer<SeparatorAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (this.Attribute.Before)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                SirenixEditorGUI.DrawThickHorizontalSeparator();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            this.CallNextDrawer(label);

            if (!this.Attribute.Before)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                SirenixEditorGUI.DrawThickHorizontalSeparator();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}
#endif
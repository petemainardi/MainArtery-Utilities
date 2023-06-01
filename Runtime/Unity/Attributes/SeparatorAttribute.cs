using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;

namespace MainArtery.Utilities.Unity.Attributes
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
    public class SeparatorAttribute : Attribute
    {
        public bool Before = true;

        public SeparatorAttribute() { }
        public SeparatorAttribute(bool placeSeparatorBefore)
        {
            Before = placeSeparatorBefore;
        }
    }

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
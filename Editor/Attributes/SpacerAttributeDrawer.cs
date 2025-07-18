#if ODIN_INSPECTOR
using System;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using MainArtery.Utilities.Unity.Attributes;


namespace MainArtery.Utilities.Unity.Editor
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Mimic the default Space attribute to make it accessible for more than just fields.
     *  Also allow a number of spaces to be specified instead of using multiple Space attributes,
     *  and whether to place the spaces before or after the item being decorated.
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public class SpacerAttributeDrawer : OdinAttributeDrawer<SpacerAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (this.Attribute.BeforeItem)
                for (int i = 0; i < this.Attribute.NumSpaces; i++)
                    EditorGUILayout.Space();

            this.CallNextDrawer(label);

            if (!this.Attribute.BeforeItem)
                for (int i = 0; i < this.Attribute.NumSpaces; i++)
                    EditorGUILayout.Space();
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}
#endif
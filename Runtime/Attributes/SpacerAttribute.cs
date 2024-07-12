#if ODIN_INSPECTOR
using System;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace MainArtery.Utilities.Unity.Attributes
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
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SpacerAttribute : ShowInInspectorAttribute
    {
        public int NumSpaces = 1;
        public bool BeforeItem = true;
        public SpacerAttribute() { }
        public SpacerAttribute(int numSpaces, bool before)
        {
            NumSpaces = Math.Max(1, numSpaces);
            BeforeItem = before;
        }
    }

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
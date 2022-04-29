using UnityEngine;
using UnityEditor;
using MainArtery.Utilities.Unity;

namespace MainArtery.Utilities.Editor
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     *  Provide in-editor execution of render functionality.
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    [CustomEditor(typeof(CaptureToTexture))]
	public class CaptureToTextureEditor : UnityEditor.Editor
	{
        // Mono ===================================================================================
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            CaptureToTexture ct = (CaptureToTexture)target;
            if (GUILayout.Button("Render to Target Image"))
                ct.RenderToTarget();

            EditorGUILayout.Space();

            if (GUILayout.Button("Render to File"))
            {
                string path = EditorUtility.SaveFilePanel("Save Image", "", ct.name + ".png", "png");
                if (path.Length > 0)
                    ct.RenderToPath(path);
            }
        }
        // ========================================================================================
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}
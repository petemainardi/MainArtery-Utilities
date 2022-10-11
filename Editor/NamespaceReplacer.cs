using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;

namespace MainArtery.Utilities.Editor
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     *  Replace the #NAMESPACE# directive in the script template with the root namespace specified
     *  by the assembly to which the created script belongs.
     *  (If not using a separate asmdef, the root namespace will be that specified under
     *  Project Settings > Editor : C# Project Generation)
     *
     *  Supposedly this should be automatically handled when setting the Root Namespace property in
     *  the project and any assembly definitions, but I have done so and it does not affect the
     *  namespace of newly-generated scripts, so here we are...
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    public class NamespaceReplacer : AssetModificationProcessor
    {
        private delegate string GetNamespaceName(string filePath);
        private static GetNamespaceName[] nameChecks =
        {
#if UNITY_2020_2_OR_NEWER
            (path) => CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath(path),
#endif
            (path) => EditorSettings.projectGenerationRootNamespace,
            (path) => PlayerSettings.productName,
            (path) => PlayerSettings.companyName,
            (path) => { string[] p = UnityEngine.Application.dataPath.Split('/'); return p[p.Length - 2]; }
        };

        public static void OnWillCreateAsset(string metaFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(metaFilePath);
            if (!fileName.EndsWith(".cs"))
                return;

            string actualFilePath = $"{Path.GetDirectoryName(metaFilePath)}{Path.DirectorySeparatorChar}{fileName}";
            string content = File.ReadAllText(actualFilePath);

            string newNamespace = nameChecks[0](actualFilePath);
            for (int i = 1; i < nameChecks.Length && string.IsNullOrWhiteSpace(newNamespace); i++)
            {
                newNamespace = String.Concat(nameChecks[i](actualFilePath).Where(c => !char.IsWhiteSpace(c)));
            }

            string newContent = content.Replace("#NAMESPACE#", newNamespace);

            if (content != newContent)
            {
                File.WriteAllText(actualFilePath, newContent);
                AssetDatabase.Refresh();
            }
        }
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}
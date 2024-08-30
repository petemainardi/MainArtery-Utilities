using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MainArtery.Utilities.Unity.Editor
{
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	/// <summary>
	/// Change the icon for gameobjects in the hierarchy tab of the editor to be that of their
	/// first non-transform component, or of the transform component if no other.
	/// </summary>
	/// <remarks>
	/// Code courtesy of <see href="https://www.youtube.com/watch?v=EFh7tniBqkk">Warped Imagination</see>.
	/// </remarks>
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	[InitializeOnLoad]
    public static class HierarchyIconDisplay
    {
		/// =======================================================================================
		/// Fields
		/// =======================================================================================
		private static bool _hierarchyHasFocus;
		private static EditorWindow _hierarchyEditorWindow;

		private static readonly Type[] SKIP_TYPES = new[] {
            typeof(Transform),
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(MeshRenderer)
        };

        /// =======================================================================================
        /// Mono
        /// =======================================================================================
        static HierarchyIconDisplay()
		{
			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
			EditorApplication.update += EditorUpdated;
		}
		
		/// =======================================================================================
		/// Methods
		/// =======================================================================================
		
		/// <summary>
		/// Save the state of whether the hierarchy tab in the editor has window focus.
		/// </summary>
		private static void EditorUpdated()
		{
			if (_hierarchyEditorWindow == null)
				_hierarchyEditorWindow = EditorWindow.GetWindow(Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));

			_hierarchyHasFocus = EditorWindow.focusedWindow != null
				&& EditorWindow.focusedWindow == _hierarchyEditorWindow;
		}

		/// <summary>
		/// Draw a component icon in the hierarchy instead of the default box icon, if able.
		/// </summary>
		private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
		{
			GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			if (go == null)
				return;

			// Skip prefab roots, still draw them as default blue box
			if (PrefabUtility.GetNearestPrefabInstanceRoot(go) == go)
				return;

			// Designate first non-transform component as displayed type if one exists, else show transform
			Component component = FirstEligibleComponent(go);
			if (component == null) return;

			Type type = component.GetType();
			GUIContent content = EditorGUIUtility.ObjectContent(component, type);
			content.text = null;
			content.tooltip = type.Name;

			// Show default if component has no associated icon
			if (content.image == null)
				return;

			// Overwrite default box
			Color color = EditorColor(
				Selection.instanceIDs.Contains(instanceID),
				selectionRect.Contains(Event.current.mousePosition),
				_hierarchyHasFocus
				);
			Rect backgroundRect = selectionRect;
			backgroundRect.width = 18.5f;
			EditorGUI.DrawRect(backgroundRect, color);



			// Draw component icon
			EditorGUI.LabelField(selectionRect, content);
		}

		/// <summary>
		/// Get the first component eligible to be considered as the representation of the specified GameObject.
		/// </summary>
		private static Component FirstEligibleComponent(GameObject go)
		{
			Component[] components = go.GetComponents<Component>();
			if (components == null || components.Length == 0)
				return null;

			Component component = components[0];
			for (int i = 1; i < components.Length; i++)
			{
				if (SKIP_TYPES.Contains(component.GetType()))
					component = components[i];
				else
					return component;
			}
			return component;
		}

		/// <summary>
		/// Determine editor background color according to event conditions.
		/// </summary>
		private static Color EditorColor(bool isSelected, bool isHovered, bool isWindowFocused)
		{
			if (isSelected)
			{
				return isWindowFocused
					? EditorGUIUtility.isProSkin
						? ColorExtensions.EditorSelectedDark
						: ColorExtensions.EditorSelectedLight
					: EditorGUIUtility.isProSkin
						? ColorExtensions.EditorSelectedUnfocusedDark
						: ColorExtensions.EditorSelectedUnfocusedLight;
            }
            else if (isHovered)
            {
                return EditorGUIUtility.isProSkin
                    ? ColorExtensions.EditorHoveredDark
                    : ColorExtensions.EditorHoveredLight;
            }
            else
            {
                return EditorGUIUtility.isProSkin
                    ? ColorExtensions.EditorDefaultDark
                    : ColorExtensions.EditorDefaultLight;
            }
        }
		/// =======================================================================================
	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}
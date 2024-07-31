using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MainArtery.Utilities.Unity.Editor
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /// <summary>
    /// Window displaying icons available to be attached to scripts to visually display type.<br/>
    /// Attach an icon to a script by selecting the script(s) in the Project tab, right-clicking,
    /// navigating to 'Create > Set Icon...', and then selecting the desired icon in the window.
    /// 
    /// </summary>
    /// <remarks>
    /// Based on code from Warped Imagination's videos for
    /// <see href="https://www.youtube.com/watch?v=YXRknQvQSio">Labels</see>,
    /// <see href="https://www.youtube.com/watch?v=SY_SdcPW8vE">Script Icons</see>,
    /// and <see href="https://www.youtube.com/watch?v=MpsO2V1Mjno">Custom Menus</see>.
    /// </remarks>
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public class SetIconWindow : EditorWindow, IHasCustomMenu
    {
		public const string TOOLS_PATH = "Tools/Script Icons";
        public const string MENU_PATH = "Assets/Utilities/Set Icon...";
		public const string LABEL = "ScriptIcon";

		/// =======================================================================================
		/// Fields
		/// =======================================================================================
		private List<Texture2D> _icons;
		private int _selectedIcon;

        /// =======================================================================================
        /// Mono
        /// =======================================================================================
		private void OnEnable()
        {
			RefreshIcons();
        }
        private void OnGUI()
        {
			if (_icons.Count == 0)
			{
				GUILayout.Label("No icons to display");
				if (GUILayout.Button("Close", GUILayout.Width(100)))
					Close();
			}
			// Display all icons from the asset database
			else
			{
				_selectedIcon = GUILayout.SelectionGrid(_selectedIcon, _icons.ToArray(), 5, GUILayout.MaxHeight(64));
				if (Event.current != null)
				{
					if (Event.current.isKey)
					{
						switch (Event.current.keyCode)
						{
							case KeyCode.KeypadEnter:
							case KeyCode.Return:
								ApplyIcon(_icons[_selectedIcon]);
								Close();
								break;
							case KeyCode.Escape:
								Close();
								break;
							default:
								break;
						}
					}
					// Check for double-click on selection
					else if (Event.current.button == 0 && Event.current.clickCount == 2)
					{
						ApplyIcon(_icons[_selectedIcon]);
						Close();
					}
				}

				GUILayout.Space(10f);
				if (GUILayout.Button("Apply", GUILayout.Width(100)))
				{
					ApplyIcon(_icons[_selectedIcon]);
					Close();
				}
			}

        }
        /// =======================================================================================
        /// Methods
        /// =======================================================================================
        private void RefreshIcons()
        {
			_icons = AssetDatabase.FindAssets($"t:texture2d l:{LABEL}")
				.Select(guid => AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid)))
				.ToList();
        }

        private void ApplyIcon(Texture2D icon)
		{
			AssetDatabase.StartAssetEditing();
			foreach (Object asset in Selection.objects)
			{
				string path = AssetDatabase.GetAssetPath(asset);
				MonoImporter importer = AssetImporter.GetAtPath(path) as MonoImporter;
				importer.SetIcon(icon);
				AssetDatabase.ImportAsset(path);

			}
			AssetDatabase.StopAssetEditing();
			AssetDatabase.Refresh();
		}
        /// =======================================================================================
        /// Menu Items
        /// =======================================================================================

        [MenuItem(MENU_PATH)]
        [MenuItem(TOOLS_PATH + "/Show Icon Assignment Window")]
        public static void ShowMenuItem()
		{
			SetIconWindow window = (SetIconWindow)EditorWindow.GetWindow(typeof(SetIconWindow));
			window.titleContent = new GUIContent("Set Icon");
			window.Show();
		}

		[MenuItem(MENU_PATH, validate = true)]
		public static bool ShowMenuItemValidation()
		{
			foreach (Object asset in Selection.objects)
			{
				if (asset.GetType() != typeof(MonoScript))
					return false;
			}
			return true;
        }
        /// ---------------------------------------------------------------------------------------

        [MenuItem(TOOLS_PATH + "/Assign Label")]
        public static void AssignScriptIconLabelMenuItem()
        {
			Object[] objects = Selection.objects.Where(obj => obj is Texture2D).ToArray();
            if (objects == null)
                return;

            foreach (Object obj in objects)
			{
				string[] labels = AssetDatabase.GetLabels(obj);
				if (!ArrayUtility.Contains<string>(labels, LABEL))
				{
					ArrayUtility.Add<string>(ref labels, LABEL);
					AssetDatabase.SetLabels(obj, labels);
				}
			}
        }

        [MenuItem(TOOLS_PATH + "/Remove Label")]
        public static void RemoveScriptIconLabelMenuItem()
        {
            Object[] objects = Selection.objects;
            if (objects == null)
                return;

            foreach (Object obj in objects)
			{
                string[] labels = AssetDatabase.GetLabels(obj);
                if (ArrayUtility.Contains<string>(labels, LABEL))
                {
                    ArrayUtility.Remove<string>(ref labels, LABEL);
                    AssetDatabase.SetLabels(obj, labels);
                }
            }
        }
        /// ---------------------------------------------------------------------------------------

        public void AddItemsToMenu(GenericMenu menu)
        {
			menu.AddItem(new GUIContent("Refresh Icons"), false, RefreshIcons);
        }

        /// =======================================================================================
    }
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}
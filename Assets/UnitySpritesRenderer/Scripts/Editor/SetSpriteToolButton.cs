using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace SpritesRenderer.Scripts.Editor
{
    public static class SetSpriteToolButton
    {
        private static GUIContent ToolbarIconCache;
        private static GUIContent ToolbarIcon
        {
            get
            {
                ToolbarIconCache ??= new GUIContent(EditorGUIUtility.IconContent("RectTool").image);
                return ToolbarIconCache;
            }
        }

        public static void Draw(Rect rect, SerializedObject serializedObject, SerializedWrapper spritePropWrapper, Transform toolParentTransform)
        {
            bool isToolActive = IsToolActive();
            using (new Utilities.ColorScope(isToolActive ? new Color(0.6f, 0.6f, 0.6f, 1f) : GUI.color))
            {
                if (GUI.Button(rect, ToolbarIcon))
                {
                    if (isToolActive) ToolManager.RestorePreviousTool();
                    else SetToolActive(serializedObject, spritePropWrapper, toolParentTransform);
                }
            }
        }
    
        private static bool IsToolActive()
        {
            return ToolManager.activeToolType == typeof(SetSpritesEditorTool);
        }
    
        private static void SetToolActive(SerializedObject serializedObject, SerializedWrapper spritePropWrapper, Transform toolParentTransform)
        {
            SetSpritesEditorTool.SetTarget(serializedObject, spritePropWrapper, toolParentTransform);
            ToolManager.SetActiveTool(typeof(SetSpritesEditorTool));
        }
    }
}
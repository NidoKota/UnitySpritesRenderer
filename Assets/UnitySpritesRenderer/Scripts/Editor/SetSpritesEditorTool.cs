using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace SpritesRenderer.Scripts.Editor
{
    public class SetSpritesEditorTool : EditorTool
    {
        private static SerializedObject TargetSerializedObject;
        private static SerializedWrapper TargetSpritePropWrapper;
        private static Transform TargetParentTransform;

        public static void SetTarget(SerializedObject serializedObject, SerializedWrapper spritePropWrapper, Transform parentTransform)
        {
            TargetSerializedObject = serializedObject;
            TargetSpritePropWrapper = spritePropWrapper;
            TargetParentTransform = parentTransform;
        }

        private SpriteProp _targetSpriteProp;
        
        public override void OnToolGUI(EditorWindow window)
        {
            if (TargetSerializedObject == null)
            {
                ToolManager.RestorePreviousTool();
                return;
            }

            TargetSerializedObject.Update();
        
            _targetSpriteProp = TargetSpritePropWrapper.GetSpriteProp();
            _targetSpriteProp = UpdateSpritePropByHandles(_targetSpriteProp);
            TargetSpritePropWrapper.SetSpriteProp(_targetSpriteProp);
        
            TargetSerializedObject.ApplyModifiedProperties();
        }
    
        private readonly TransformHandles _handles = new TransformHandles();
    
        private SpriteProp UpdateSpritePropByHandles(SpriteProp spriteProp)
        {
            Vector3 position = spriteProp.Position / 2;
            Quaternion rotation = Quaternion.AngleAxis(spriteProp.Rotation, Vector3.forward);
            Vector3 size = spriteProp.PureScale;

            position += TargetParentTransform.position;
            
            using (new Handles.DrawingScope(new Color32(73, 170, 224, 255)))
            {
                _handles.DrawArrowHandleUp(ref position, ref rotation);
                _handles.DrawArrowHandleRight(ref position, ref rotation);
                _handles.DrawRectangleHandle(ref position, ref rotation);
                _handles.DrawBoxBoundsHandle(ref position, ref rotation, ref size);
                _handles.DrawRotateHandle(ref position, ref rotation);
            }

            position -= TargetParentTransform.position;
            
            spriteProp.SetPosition(position * 2);
            spriteProp.SetRotation(rotation.eulerAngles.z);
            spriteProp.SetScale(size);

            return spriteProp;
        }
    }
}
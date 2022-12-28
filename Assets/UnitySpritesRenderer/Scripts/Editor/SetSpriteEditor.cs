using UnityEditor;
using UnityEngine;

namespace SpritesRenderer.Scripts.Editor
{
    [CustomEditor(typeof(SetSprites))]
    public class SetSpritesEditor : UnityEditor.Editor
    {
        private SpritePropsReorderableListWrapper _spritePropsReorderableListWrapper;
        private SerializedProperty _spritePropWrappersProp;
        private SerializedProperty _targetMaterialProp;
        private SetSprites _targetSetSprites;

        [DrawGizmo(GizmoType.Selected | GizmoType.Selected, typeof(SetSprites))]
        private static void DrawGizmo(SetSprites setSprits, GizmoType gizmoType)
        {
            Gizmos.color = new Color32(124, 188, 224, 255);

            SerializedObject so = new SerializedObject(setSprits);
            SerializedProperty spritePropWrappersProp = so.FindProperty("_spritePropWrappers");

            var count = spritePropWrappersProp.arraySize;
            for (var i = 0; i < count; i++)
            {
                SerializedProperty currentSpritePropWrapper = spritePropWrappersProp.GetArrayElementAtIndex(i);
                
                if(!currentSpritePropWrapper.objectReferenceValue) continue;
                
                SerializedObject spritePropWrappersSerializedObject = new SerializedObject(currentSpritePropWrapper.objectReferenceValue);
                SerializedProperty spritePropProp = spritePropWrappersSerializedObject.FindProperty("_spriteProp");
                SpriteProp spriteProp = new SerializedWrapper(spritePropProp).GetSpriteProp();

                Vector2 position = spriteProp.Position / 2 + (Vector2)setSprits.transform.position;
                Vector2 scale = spriteProp.Scale / 2;
                
                Vector3 rightUp = position + scale;
                Vector3 rightDown = position + new Vector2(scale.x, -scale.y);
                Vector3 leftUp = position + new Vector2(-scale.x, scale.y);
                Vector3 leftDown = position - scale;
            
                Gizmos.DrawLine(rightUp, rightDown);
                Gizmos.DrawLine(rightDown, leftDown);
                Gizmos.DrawLine(leftDown, leftUp);
                Gizmos.DrawLine(leftUp, rightUp);
            }
        }

        private void OnEnable()
        {
            _spritePropWrappersProp = serializedObject.FindProperty("_spritePropWrappers");
            _targetMaterialProp = serializedObject.FindProperty("_targetMaterial");
            _targetSetSprites = target as SetSprites;
            _spritePropsReorderableListWrapper = new SpritePropsReorderableListWrapper(serializedObject, _spritePropWrappersProp, _targetSetSprites.transform);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_targetMaterialProp);
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            _spritePropsReorderableListWrapper.Draw();

            serializedObject.ApplyModifiedProperties();
        }
    }
}

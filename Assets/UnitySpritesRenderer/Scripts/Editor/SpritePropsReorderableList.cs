using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEditorInternal;
using UnityEngine;

namespace UnitySpritesRenderer.Editor
{
    public class SpritePropsReorderableList
    {
        private readonly SerializedObject _serializedObject;
        private readonly SerializedProperty _spritePropWrappersProp;
        private readonly ReorderableList _spritePropReorderableList;
        private readonly Transform _toolParentTransform;
        private const string _listName = "SpritePropsList";

        public SpritePropsReorderableList(SerializedObject serializedObject, SerializedProperty spritePropWrappersProp, Transform toolParentTransform)
        {
            _serializedObject = serializedObject;
            _spritePropWrappersProp = spritePropWrappersProp;
            _spritePropReorderableList = GetSpritePropReorderableList();
            _toolParentTransform = toolParentTransform;
        }
    
        public void Draw()
        {
            if (_spritePropWrappersProp.isExpanded) _spritePropReorderableList.DoLayoutList();
            else _spritePropWrappersProp.isExpanded = EditorGUILayout.Foldout(_spritePropWrappersProp.isExpanded, _listName, true);
        }

        private ReorderableList GetSpritePropReorderableList()
        {
            ReorderableList rl = new ReorderableList(_serializedObject, _spritePropWrappersProp);

            rl.onAddCallback = OnAdd;
            rl.drawHeaderCallback = DrawHeader;
            rl.elementHeightCallback = ElementHeight;
            rl.drawElementCallback = DrawElement;
        
            return rl;
        }
        
        private void OnAdd(ReorderableList reorderableList)
        {
            _spritePropWrappersProp.arraySize++;
        }
        
        private void DrawHeader(Rect rect)
        {
            rect = new Rect(rect) { xMin = rect.xMin - 8, xMax = rect.xMax - 50 };
            _spritePropWrappersProp.isExpanded = EditorGUI.Foldout(rect, _spritePropWrappersProp.isExpanded, _listName, true);
        }
    
        private float ElementHeight(int index)
        {
            SerializedProperty currentProp = _spritePropWrappersProp.GetArrayElementAtIndex(index);
            if (!currentProp.objectReferenceValue || !currentProp.isExpanded) return EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight * 10;
        }
    
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty currentProp = _spritePropWrappersProp.GetArrayElementAtIndex(index);
            SerializedObject wrapperSerializedObject = currentProp.objectReferenceValue ? new SerializedObject(currentProp.objectReferenceValue) : null;

            Rect lineRect = Utilities.LineRectUpdate(new Rect(rect) { yMax = rect.yMin });
            DrawSpriteProp(lineRect, currentProp, wrapperSerializedObject);
            
            wrapperSerializedObject?.ApplyModifiedProperties();
        }

        private void DrawSpriteProp(Rect lineRect, SerializedProperty currentProp, SerializedObject wrapperSerializedObject)
        {
            SerializedProperty spritePropProp = null;
            SerializedProperty activeProp = null;

            if (wrapperSerializedObject != null)
            {
                spritePropProp = wrapperSerializedObject.FindProperty("_spriteProp");
                activeProp = spritePropProp.FindPropertyRelative("_active");
                
                if (currentProp.isExpanded) Utilities.LineRectUpdatePropertyField(Utilities.LineRectUpdate(lineRect), spritePropProp.Copy(), true, null, "_active");
            }

            DrawSpritePropHeader(lineRect, currentProp, wrapperSerializedObject, spritePropProp, activeProp);
        }
        
        private void DrawSpritePropHeader(Rect lineRect, SerializedProperty currentProp, SerializedObject wrapperSerializedObject, SerializedProperty spritePropProp, SerializedProperty activeProp)
        {
            Rect leftRect = new Rect(lineRect) {xMax = lineRect.xMin + 60};
            Rect rightRect = new Rect(lineRect) {xMin = leftRect.xMax + 15};
            
            currentProp.isExpanded = EditorGUI.Foldout(Utilities.GetHorizonLayout(rightRect, 0, 2f, 5f), currentProp.isExpanded, currentProp.displayName, true);
            EditorGUI.PropertyField(Utilities.GetHorizonLayout(rightRect, 1, 2f, 5f), currentProp, GUIContent.none, false);
            
            if (wrapperSerializedObject == null) return;

            activeProp.boolValue = EditorGUI.Toggle(Utilities.GetHorizonLayout(leftRect, 0, 2f, 5f), activeProp.boolValue);
            SetSpriteToolButton.Draw(Utilities.GetHorizonLayout(leftRect, 1, 2f, 5f), wrapperSerializedObject, new SerializedWrapper(spritePropProp), _toolParentTransform);
        }
    }
}
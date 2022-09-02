using UnityEditor;

namespace UnitySpritesRenderer.Editor
{
    public struct SerializedWrapper
    {
        private SerializedObject _serializedObject;
        private SerializedProperty _serializedProperty;
        
        public SerializedWrapper(SerializedObject serializedObject)
        {
            _serializedObject = serializedObject;
            _serializedProperty = null;
        }
        
        public SerializedWrapper(SerializedProperty serializedProperty)
        {
            _serializedObject = null;
            _serializedProperty = serializedProperty;
        }

        public SerializedProperty FindProperty(string propertyPath)
        {
            return _serializedObject == null ? _serializedProperty.FindPropertyRelative(propertyPath) : _serializedObject.FindProperty(propertyPath);
        }

        public SerializedProperty GetIterator()
        {
            return _serializedObject == null ? _serializedProperty : _serializedObject.GetIterator();
        }
    }
}
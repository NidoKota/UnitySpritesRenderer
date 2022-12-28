using UnityEditor;
using UnityEngine;

namespace SpritesRenderer.Scripts.Editor
{
    public static class SerializedWrapperExtensionMethods
    {
        public static SpriteProp GetSpriteProp(this SerializedWrapper serializedWrapper)
        {
            SerializedProperty positionProperty = serializedWrapper.FindProperty("_position");
            SerializedProperty rotationProperty = serializedWrapper.FindProperty("_rotation");
            SerializedProperty scaleProperty = serializedWrapper.FindProperty("_scale");
            SerializedProperty flipXProperty = serializedWrapper.FindProperty("_flipX");
            SerializedProperty flipYProperty = serializedWrapper.FindProperty("_flipY");
            SerializedProperty spriteProperty = serializedWrapper.FindProperty("_sprite");
            SerializedProperty tileProperty = serializedWrapper.FindProperty("_tile");
            SerializedProperty offsetProperty = serializedWrapper.FindProperty("_offset");
            SerializedProperty alphaProperty = serializedWrapper.FindProperty("_alpha");
            SerializedProperty activeProperty = serializedWrapper.FindProperty("_active");

            if (spriteProperty.objectReferenceValue)
                return new SpriteProp(
                    positionProperty.vector2Value,
                    rotationProperty.floatValue,
                    scaleProperty.vector2Value,
                    flipXProperty.boolValue,
                    flipYProperty.boolValue,
                    spriteProperty.objectReferenceValue as Sprite,
                    alphaProperty.floatValue,
                    activeProperty.boolValue);


            return new SpriteProp(
                positionProperty.vector2Value,
                rotationProperty.floatValue,
                scaleProperty.vector2Value,
                flipXProperty.boolValue,
                flipYProperty.boolValue,
                tileProperty.vector2Value,
                offsetProperty.vector2Value,
                alphaProperty.floatValue,
                activeProperty.boolValue);
        }

        public static void SetSpriteProp(this SerializedWrapper serializedWrapper, SpriteProp spriteProp)
        {
            SerializedProperty positionProperty = serializedWrapper.FindProperty("_position");
            SerializedProperty rotationProperty = serializedWrapper.FindProperty("_rotation");
            SerializedProperty scaleProperty = serializedWrapper.FindProperty("_scale");
            SerializedProperty flipXProperty = serializedWrapper.FindProperty("_flipX");
            SerializedProperty flipYProperty = serializedWrapper.FindProperty("_flipY");
            SerializedProperty tileProperty = serializedWrapper.FindProperty("_tile");
            SerializedProperty offsetProperty = serializedWrapper.FindProperty("_offset");
            SerializedProperty spriteProperty = serializedWrapper.FindProperty("_sprite");
            SerializedProperty alphaProperty = serializedWrapper.FindProperty("_alpha");
            SerializedProperty activeProperty = serializedWrapper.FindProperty("_active");

            positionProperty.vector2Value = spriteProp.Position;
            rotationProperty.floatValue = spriteProp.Rotation;
            scaleProperty.vector2Value = spriteProp.PureScale;
            flipXProperty.boolValue = spriteProp.FlipX;
            flipYProperty.boolValue = spriteProp.FlipY;
            alphaProperty.floatValue = spriteProp.Alpha;
            activeProperty.boolValue = spriteProp.Active;

            if (spriteProperty.objectReferenceValue)
                spriteProperty.objectReferenceValue = spriteProp.Sprite;
            else
            {
                tileProperty.vector2Value = spriteProp.Tile;
                offsetProperty.vector2Value = spriteProp.Offset;
            }
        }
    }
}
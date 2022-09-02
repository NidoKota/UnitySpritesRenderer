using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnitySpritesRenderer.Editor
{
    public static class Utilities
    {
        public static Rect LineRectUpdatePropertyField(Rect lineRect, SerializedProperty propIterator, bool isSerializedProps, GUIContent label = null, params string[] propertyToExclude)
        {
            if (isSerializedProps) propIterator.NextVisible(true);
            do
            {
                if (propertyToExclude.Any(x => x == propIterator.name)) continue;
                EditorGUI.PropertyField(lineRect, propIterator, label, false);
                lineRect = LineRectUpdate(lineRect);
            } while (isSerializedProps && propIterator.NextVisible(false));

            return lineRect;
        }

        public static Rect LineRectUpdate(Rect lineRect)
        {
            return new Rect(lineRect) {yMin = lineRect.yMax, yMax = lineRect.yMax + EditorGUIUtility.singleLineHeight};
        }

        public static Rect GetHorizonLayout(Rect rect, int index, params float[] ratios)
        {
            float sum = ratios.Sum();
            float leftRatios = ratios.Where((x, i) => i < index).Sum() / sum;
            float rightRatios = ratios.Where((x, i) => i > index).Sum() / sum;
            float width = rect.width;
            return new Rect(rect) {xMin = rect.xMin + width * leftRatios, xMax = rect.xMin + width * (1f - rightRatios)};
        }
        
        public class ColorScope : GUI.Scope
        {
            private readonly Color _colorCache;
    
            public ColorScope(Color color)
            {
                _colorCache = GUI.color;
                GUI.color = color;
            }

            protected override void CloseScope()
            {
                GUI.color = _colorCache;
            }
        }
    }
}
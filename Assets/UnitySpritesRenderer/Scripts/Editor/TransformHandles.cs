using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UnitySpritesRenderer.Editor
{
    public class TransformHandles
    {
        public float HandleSnap { get; private set; } = 0.1f;
        public void SetHandleSnap(float value)
        {
            HandleSnap = value;
        }
    
        private Vector3 GetHandleUpDirection(Quaternion rotation) => rotation * Vector3.up;
        private Vector3 GetHandleRightDirection(Quaternion rotation) => rotation * Vector3.right;
        private Matrix4x4 GetRotatedHandleMatrix(Quaternion rotation) => Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
    
        private float GetArrowHandleSize(Vector3 position, float handleSize) => HandleUtility.GetHandleSize(position) * handleSize;

        public void DrawArrowHandleUp(ref Vector3 position, ref Quaternion rotation, float handleSize = 0.8f)
        {
            position = Handles.Slider(
                position,
                GetHandleUpDirection(rotation),
                GetArrowHandleSize(position, handleSize),
                Handles.ArrowHandleCap,
                HandleSnap);
        }

        public void DrawArrowHandleRight(ref Vector3 position, ref Quaternion rotation, float handleSize = 0.8f)
        {
            position = Handles.Slider(
                position,
                GetHandleRightDirection(rotation),
                GetArrowHandleSize(position, handleSize),
                Handles.ArrowHandleCap,
                HandleSnap);
        }
    
        private int? _rectangleHandleIdCache;
        private float GetRectangleHandleSize(Vector3 position, float handleSize) => HandleUtility.GetHandleSize(position) * handleSize;
        private int GetRectangleHandleId() => _rectangleHandleIdCache ?? (_rectangleHandleIdCache = GUIUtility.GetControlID(FocusType.Passive)).Value;
        private Vector3 GetRectangleHandleOffset(Vector3 position, Quaternion rotation, float handleSize) => rotation * new Vector3(GetRectangleHandleSize(position, handleSize), GetRectangleHandleSize(position, handleSize));

        public void DrawRectangleHandle(ref Vector3 position, ref Quaternion rotation, float handleSize = 0.1f)
        {
            position = Handles.Slider2D(
                GetRectangleHandleId(),
                position,
                GetRectangleHandleOffset(position, rotation, handleSize),
                Vector3.forward,
                GetHandleUpDirection(rotation),
                GetHandleRightDirection(rotation),
                GetRectangleHandleSize(position, handleSize),
                Handles.RectangleHandleCap,
                new Vector2(HandleSnap, HandleSnap));
        }

        private readonly BoxBoundsHandle _boundsHandle = new BoxBoundsHandle {axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Y};

        public void DrawBoxBoundsHandle(ref Vector3 position, ref Quaternion rotation, ref Vector3 size)
        {
            using (var ccs = new EditorGUI.ChangeCheckScope())
            {
                Matrix4x4 matrix = GetRotatedHandleMatrix(rotation);
                using (new Handles.DrawingScope(matrix))
                {
                    _boundsHandle.center = matrix.inverse.MultiplyPoint3x4(position);
                    _boundsHandle.size = size;

                    _boundsHandle.DrawHandle();

                    if (ccs.changed)
                    {
                        position = matrix.MultiplyPoint3x4(_boundsHandle.center);
                        size = _boundsHandle.size;
                    }
                }
            }
        }

        private float GetRotateHandleSize(Vector3 position, float handleSize) => HandleUtility.GetHandleSize(position) * handleSize;

        public void DrawRotateHandle(ref Vector3 position, ref Quaternion rotation, float handleSize = 1f)
        {
            rotation = Handles.Disc(
                rotation,
                position,
                Vector3.forward,
                GetRotateHandleSize(position, handleSize),
                false,
                HandleSnap);
        }
    }
}
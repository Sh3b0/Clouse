using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BezierCurveComponent))]
    public class GeneralBezierCurvePreviewEditor : UnityEditor.Editor
    {
        public static bool DrawTangents = true;
        public static bool EditPoints = true;
        
        private BezierCurveComponent _target;

        private void OnEnable()
        {
            _target = (BezierCurveComponent) target;
        }

        private void OnSceneGUI()
        {
            if (DrawTangents)
                DrawCurveTangents();
            
            if (EditPoints)
                DrawPointHandles();
            
            DrawCurve();
            DrawButtons();
        }

        private void DrawCurveTangents()
        {
            Handles.color = Color.yellow * 0.5f;
            Handles.DrawPolyLine(_target.Points);
        }

        private void DrawPointHandles()
        {
            var points = _target.Points;

            for (var index = 0; index < points.Length; index++)
            {
                EditorGUI.BeginChangeCheck();
                var newPoint = Handles.PositionHandle(points[index], Quaternion.identity);
                if (!EditorGUI.EndChangeCheck()) continue;

                Undo.RecordObject(_target, "Changed Bezier Curve point");
                _target.Points[index] = newPoint;
            }
        }

        private void DrawCurve()
        {
            var points = Mathf.RoundToInt(GetTotalDistance());
            var step = 1f / (points - 1);
            Handles.color = Color.green;

            for (var index = 1; index < points; index++)
            {
                var from = _target.EvaluateAt(step * (index - 1));
                var to = _target.EvaluateAt(step * index);
                Handles.DrawLine(from, to);
            }
        }

        private float GetTotalDistance()
        {
            var totalDistance = 0f;
            
            for (var i = 1; i < _target.Points.Length; i++)
            {
                var from = _target.Points[i - 1];
                var to = _target.Points[i];
                var distance = Vector3.Distance(from, to);

                totalDistance += distance;
            }

            return totalDistance;
        }

        private void DrawButtons()
        {
            var rect = new Rect(Screen.width - 150, Screen.height - 150, 120, 100);
            GUILayout.Window(0, rect, DrawMenu, "Bezier Curve");
        }

        private void DrawMenu(int id)
        {
            DrawToggles();
            DrawPlusButton();
        }

        private static void DrawToggles()
        {
            DrawTangents = GUILayout.Toggle(DrawTangents, "Draw Tangents");
            EditPoints = GUILayout.Toggle(EditPoints, "Edit Points");
        }

        private void DrawPlusButton()
        {
            if (!GUILayout.Button("+")) return;

            var points = _target.Points;
            var lastPoint = points[points.Length - 1];
            var newPoints = points
                .Concat(new[] {lastPoint})
                .ToArray();

            Undo.RecordObject(_target, "Created a new Bezier Curve point");
            _target.Points = newPoints;
        }
    }
}
using UnityEngine;
using AzurProject.Movement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AzurProject.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(BezierMovement), true)]
    public class BezierRoutesPointDrawer : UnityEditor.Editor
    {
        private readonly GUIStyle _style = new GUIStyle();

        private void OnEnable()
        {
            _style.fontStyle = FontStyle.Bold;
            _style.normal.textColor = Color.white;
        }

        public void OnSceneGUI()
        {
            BezierMovement bezierMovement = (BezierMovement) target;

            if (bezierMovement == null)
            {
                return;
            }

            BezierRoute[] bezierRoutes = bezierMovement.bezierRoutes;

            if (bezierRoutes == null)
            {
                return;
            }

            for (int index = 0; index < bezierRoutes.Length; index++)
            {
                var bezierRoute = bezierRoutes[index];

                Handles.Label(bezierRoute.point0, "route_" + index + "[0]");
                Handles.Label(bezierRoute.point1, "route_" + index + "[1]");
                Handles.Label(bezierRoute.point2, "route_" + index + "[2]");
                Handles.Label(bezierRoute.point3, "route_" + index + "[3]");

                bezierRoute.point0 = Handles.PositionHandle(bezierRoute.point0, Quaternion.identity);
                bezierRoute.point1 = Handles.PositionHandle(bezierRoute.point1, Quaternion.identity);
                bezierRoute.point2 = Handles.PositionHandle(bezierRoute.point2, Quaternion.identity);
                bezierRoute.point3 = Handles.PositionHandle(bezierRoute.point3, Quaternion.identity);

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif
}

using UnityEngine;
using AzurProject.Movement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AzurProject.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(LinearMovement), true)]
    public class LinearMovementPointDrawer : UnityEditor.Editor
    {
        private readonly GUIStyle _style = new GUIStyle();

        private void OnEnable()
        {
            _style.fontStyle = FontStyle.Bold;
            _style.normal.textColor = Color.white;
        }

        public void OnSceneGUI()
        {
            LinearMovement linearMovement = (LinearMovement) target;

            if (linearMovement == null)
            {
                return;
            }

            LinearMovementCoordinate[] linearMovementCoordinates = linearMovement.coordinates;

            if (linearMovementCoordinates == null)
            {
                return; 
            }
            
            for (int index = 0; index < linearMovementCoordinates.Length; index++)
            {
                var linearMovementCoordinate = linearMovementCoordinates[index];
                
                Handles.Label(linearMovementCoordinate.position, "point_" + index);

                linearMovementCoordinate.position = Handles.PositionHandle(linearMovementCoordinate.position, Quaternion.identity);
                
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif
}

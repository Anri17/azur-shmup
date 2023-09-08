#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using AzurShmup.Stage;

namespace AzurShmup.Bullet
{
    [CustomEditor(typeof(ShotGeneratorField))]
    public class ShotGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            if (GUILayout.Button("Reset Shot"))
            {
                ShotGeneratorField shotGenerator = (ShotGeneratorField)target;

                shotGenerator.StopShot();
                shotGenerator.StartShot();
            }
        }
    }
}
#endif

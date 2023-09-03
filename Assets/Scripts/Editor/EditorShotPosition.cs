namespace AzurShmup.Editor
{
    // [CustomEditor(typeof(BulletSpawnerPosition))]
    public class EditorShotPosition : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            /*
            BulletSpawnerPosition bulletSpawnerPosition = (BulletSpawnerPosition) target;

            bulletSpawnerPosition.shotPositionType = (ShotPositionType) EditorGUILayout.EnumPopup(bulletSpawnerPosition.shotPositionType);
            
            switch (bulletSpawnerPosition.shotPositionType)
            {
                case ShotPositionType.RANDOM:
                    bulletSpawnerPosition.minPos = EditorGUILayout.Vector2Field("Minimum Limit", bulletSpawnerPosition.minPos);
                    bulletSpawnerPosition.maxPos = EditorGUILayout.Vector2Field("Maximum Limit", bulletSpawnerPosition.maxPos);
                    
                    break;
                case ShotPositionType.SPECIFIC:
                    bulletSpawnerPosition.specificShotPosition =
                        EditorGUILayout.Vector2Field("Position", bulletSpawnerPosition.specificShotPosition);
                    
                    break;
                case ShotPositionType.NONE:
                    EditorGUILayout.LabelField($"Position: {bulletSpawnerPosition.transform.position}");
                    break;
                default:
                    EditorGUILayout.LabelField("This should not be selected. if it is, something is broken in the ShotPositionEditor, or ShotPosition script");
                    break;
            }
            */
        }
    }
}
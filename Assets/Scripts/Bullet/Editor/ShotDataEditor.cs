#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AzurShmup.Bullet
{
    [CustomEditor(typeof(ShotData))]
    public class ShotDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ShotData shotData = (ShotData)target;

            EditorGUILayout.Space();
            shotData.bulletGraphicType = (BulletGraphicType)EditorGUILayout.EnumPopup("Bullet Graphic", shotData.bulletGraphicType);

            EditorGUILayout.Space();
            shotData.bulletSpawnDelay = EditorGUILayout.FloatField("Bullet Spawn Delay", shotData.bulletSpawnDelay);

            EditorGUILayout.Space();
            shotData.bulletSpawnPositionType = (BulletSpawnPositionType)EditorGUILayout.EnumPopup("Bullet Spawn Position", shotData.bulletSpawnPositionType);
            if (shotData.bulletSpawnPositionType == BulletSpawnPositionType.SPECIFIC)
            {
                shotData.bulletSpawnPosition.type = BulletSpawnPositionType.SPECIFIC;
                shotData.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shotData.bulletSpawnPosition.position);
                shotData.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shotData.bulletSpawnPosition.offset);
            }
            else if (shotData.bulletSpawnPositionType == BulletSpawnPositionType.PARENT)
            {
                shotData.bulletSpawnPosition.type = BulletSpawnPositionType.PARENT;
                shotData.bulletSpawnPosition.position = shotData.transform.position;
                GUI.enabled = false; // show only
                shotData.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shotData.bulletSpawnPosition.position);
                GUI.enabled = true;
                shotData.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shotData.bulletSpawnPosition.offset);
            }

            EditorGUILayout.Space();
            shotData.bulletBehaviourType = (BulletBehaviourType)EditorGUILayout.EnumPopup("Bullet Behaviour Type", shotData.bulletBehaviourType);
            if (shotData.bulletBehaviourType == BulletBehaviourType.BASIC_A)
            {
                shotData.bulletBehaviour.type = BulletBehaviourType.BASIC_A;
                shotData.bulletBehaviour.basicA.angle = EditorGUILayout.FloatField("Angle", shotData.bulletBehaviour.basicA.angle);
                shotData.bulletBehaviour.basicA.speed = EditorGUILayout.FloatField("Speed", shotData.bulletBehaviour.basicA.speed);
            }
            else if (shotData.bulletBehaviourType == BulletBehaviourType.BASIC_B)
            {
                shotData.bulletBehaviour.type = BulletBehaviourType.BASIC_B;
                shotData.bulletBehaviour.basicB.speed = EditorGUILayout.Vector2Field("Speed", shotData.bulletBehaviour.basicB.speed);
            }

            EditorGUILayout.Space();
            shotData.shotPatternType = (ShotPatternType)EditorGUILayout.EnumPopup("Shot Pattern Type", shotData.shotPatternType);
            if (shotData.shotPatternType == ShotPatternType.LINEAR)
            {
                shotData.shotPattern.type = ShotPatternType.LINEAR;
                shotData.shotPattern.linear.bulletCount = EditorGUILayout.FloatField("Bullet Count", shotData.shotPattern.linear.bulletCount);
                shotData.shotPattern.linear.loopDelay = EditorGUILayout.FloatField("Loop Delay", shotData.shotPattern.linear.loopDelay);
                shotData.shotPattern.linear.startDelay = EditorGUILayout.FloatField("Start Delay", shotData.shotPattern.linear.startDelay);
            }

            EditorGUILayout.Space();
            shotData.loopShot = EditorGUILayout.Toggle("Loop Shot?", shotData.loopShot);
            if (shotData.loopShot)
            {
                shotData.loopDelay = EditorGUILayout.FloatField("Loop Delay", shotData.loopDelay);
            }
        }
    }
}

#endif
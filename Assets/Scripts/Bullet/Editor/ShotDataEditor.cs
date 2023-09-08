#if UNITY_EDITOR
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace AzurShmup.Bullet
{
    [CustomEditor(typeof(ShotData))]
    public class ShotDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            ShotData shotData = (ShotData)target;

            shotData.shot.type = (ShotType)EditorGUILayout.EnumPopup("Shot Type", shotData.shot.type);
            EditorGUILayout.Space();

            switch (shotData.shot.type)
            {
                case ShotType.LINEAR_BASIC_A: Inspector_LinearBasicA(shotData.transform, ref shotData.shot.linearBasicA); break;
                case ShotType.LINEAR_BASIC_B: Inspector_LinearBasicB(shotData.transform, ref shotData.shot.linearBasicB); break;
                case ShotType.LINEAR_ACCELERATING_A: Inspector_LinearAcceleratingA(shotData.transform, ref shotData.shot.linearAcceleratingA); break;
                case ShotType.CIRCULAR_BASIC_A: Inspector_CircularBasicA(shotData.transform, ref shotData.shot.circularBasicA); break;
            }

        }

        private void Inspector_LinearBasicA(Transform shotTransform, ref ShotLinearBasicA shot)
        {
            shot.bulletGraphic = (BulletGraphic)EditorGUILayout.EnumPopup("Bullet Graphic", shot.bulletGraphic);
            shot.bulletSpawnDelay = EditorGUILayout.FloatField("Bullet Spawn Delay", shot.bulletSpawnDelay);

            shot.bulletSpawnPosition.type = (BulletSpawnPositionType)EditorGUILayout.EnumPopup("Bullet Spawn Position", shot.bulletSpawnPosition.type);
            switch (shot.bulletSpawnPosition.type)
            {
                case BulletSpawnPositionType.PARENT:
                    shot.bulletSpawnPosition.position = shotTransform.position;
                    GUI.enabled = false; // show only
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    GUI.enabled = true;
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
                case BulletSpawnPositionType.SPECIFIC:
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
            }
            EditorGUILayout.Space();

            shot.bulletBehaviour.angle = EditorGUILayout.FloatField("Angle", shot.bulletBehaviour.angle);
            shot.bulletBehaviour.speed = EditorGUILayout.FloatField("Speed", shot.bulletBehaviour.speed);
            EditorGUILayout.Space();

            shot.start_delay = EditorGUILayout.FloatField("Start Delay", shot.start_delay);
            shot.shoot_delay = EditorGUILayout.FloatField("Shoot Delay", shot.shoot_delay);
            shot.is_infinite_shots = EditorGUILayout.Toggle("Infinite Shots?", shot.is_infinite_shots);
            if (!shot.is_infinite_shots)
                shot.shoot_count = EditorGUILayout.FloatField("Shoot Count", shot.shoot_count);

            EditorGUILayout.Space();
            shot.loop_shot = EditorGUILayout.Toggle("Loop Shot?", shot.loop_shot);
            if (shot.loop_shot)
            {
                shot.loop_delay = EditorGUILayout.FloatField("Loop Delay", shot.loop_delay);
            }
        }

        private void Inspector_LinearBasicB(Transform shotTransform, ref ShotLinearBasicB shot)
        {
            shot.bulletGraphic = (BulletGraphic)EditorGUILayout.EnumPopup("Bullet Graphic", shot.bulletGraphic);
            shot.bulletSpawnDelay = EditorGUILayout.FloatField("Bullet Spawn Delay", shot.bulletSpawnDelay);

            shot.bulletSpawnPosition.type = (BulletSpawnPositionType)EditorGUILayout.EnumPopup("Bullet Spawn Position", shot.bulletSpawnPosition.type);
            switch (shot.bulletSpawnPosition.type)
            {
                case BulletSpawnPositionType.PARENT:
                    shot.bulletSpawnPosition.position = shotTransform.position;
                    GUI.enabled = false; // show only
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    GUI.enabled = true;
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
                case BulletSpawnPositionType.SPECIFIC:
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
            }
            EditorGUILayout.Space();

            shot.bulletBehaviour.speed = EditorGUILayout.Vector2Field("Speed", shot.bulletBehaviour.speed);
            EditorGUILayout.Space();

            shot.start_delay = EditorGUILayout.FloatField("Start Delay", shot.start_delay);
            shot.shoot_delay = EditorGUILayout.FloatField("Shoot Delay", shot.shoot_delay);
            shot.is_infinite_shots = EditorGUILayout.Toggle("Infinite Shots?", shot.is_infinite_shots);
            if (!shot.is_infinite_shots)
                shot.shoot_count = EditorGUILayout.FloatField("Shoot Count", shot.shoot_count);

            EditorGUILayout.Space();
            shot.loop_shot = EditorGUILayout.Toggle("Loop Shot?", shot.loop_shot);
            if (shot.loop_shot)
            {
                shot.loop_delay = EditorGUILayout.FloatField("Loop Delay", shot.loop_delay);
            }
        }

        private void Inspector_LinearAcceleratingA(Transform shotTransform, ref ShotLinearAcceleratingA shot)
        {
            shot.bulletGraphic = (BulletGraphic)EditorGUILayout.EnumPopup("Bullet Graphic", shot.bulletGraphic);
            shot.bulletSpawnDelay = EditorGUILayout.FloatField("Bullet Spawn Delay", shot.bulletSpawnDelay);

            shot.bulletSpawnPosition.type = (BulletSpawnPositionType)EditorGUILayout.EnumPopup("Bullet Spawn Position", shot.bulletSpawnPosition.type);
            switch (shot.bulletSpawnPosition.type)
            {
                case BulletSpawnPositionType.PARENT:
                    shot.bulletSpawnPosition.position = shotTransform.position;
                    GUI.enabled = false; // show only
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    GUI.enabled = true;
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
                case BulletSpawnPositionType.SPECIFIC:
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
            }
            EditorGUILayout.Space();

            shot.bulletBehaviour.angle = EditorGUILayout.FloatField("Angle", shot.bulletBehaviour.angle);
            shot.bulletBehaviour.speed = EditorGUILayout.FloatField("Speed", shot.bulletBehaviour.speed);
            shot.bulletBehaviour.angle_change = EditorGUILayout.FloatField("Angle Change", shot.bulletBehaviour.angle_change);
            shot.bulletBehaviour.speed_change = EditorGUILayout.FloatField("Speed Change", shot.bulletBehaviour.speed_change);
            shot.bulletBehaviour.speed_max = EditorGUILayout.FloatField("Speed Max", shot.bulletBehaviour.speed_max);
            EditorGUILayout.Space();

            shot.start_delay = EditorGUILayout.FloatField("Start Delay", shot.start_delay);
            shot.shoot_delay = EditorGUILayout.FloatField("Shoot Delay", shot.shoot_delay);
            shot.is_infinite_shots = EditorGUILayout.Toggle("Infinite Shots?", shot.is_infinite_shots);
            if (!shot.is_infinite_shots)
                shot.shoot_count = EditorGUILayout.FloatField("Shoot Count", shot.shoot_count);

            EditorGUILayout.Space();
            shot.loop_shot = EditorGUILayout.Toggle("Loop Shot?", shot.loop_shot);
            if (shot.loop_shot)
            {
                shot.loop_delay = EditorGUILayout.FloatField("Loop Delay", shot.loop_delay);
            }
        }

        private void Inspector_CircularBasicA(Transform shotTransform, ref ShotCircularBasicA shot)
        {
            shot.bulletGraphic = (BulletGraphic)EditorGUILayout.EnumPopup("Bullet Graphic", shot.bulletGraphic);
            shot.bulletSpawnDelay = EditorGUILayout.FloatField("Bullet Spawn Delay", shot.bulletSpawnDelay);

            shot.bulletSpawnPosition.type = (BulletSpawnPositionType)EditorGUILayout.EnumPopup("Bullet Spawn Position", shot.bulletSpawnPosition.type);
            switch (shot.bulletSpawnPosition.type)
            {
                case BulletSpawnPositionType.PARENT:
                    shot.bulletSpawnPosition.position = shotTransform.position;
                    GUI.enabled = false; // show only
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    GUI.enabled = true;
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
                case BulletSpawnPositionType.SPECIFIC:
                    shot.bulletSpawnPosition.position = EditorGUILayout.Vector2Field("Spawn Position", shot.bulletSpawnPosition.position);
                    shot.bulletSpawnPosition.offset = EditorGUILayout.Vector2Field("Offset", shot.bulletSpawnPosition.offset);
                    break;
            }
            EditorGUILayout.Space();

            shot.is_random_directions = EditorGUILayout.Toggle("Random Directions?", shot.is_random_directions);
            if (!shot.is_random_directions)
            {
                shot.shot_directions = EditorGUILayout.IntField("Shot Directions", shot.shot_directions);
            }
            shot.start_angle = EditorGUILayout.FloatField("Start Angle", shot.start_angle);
            shot.end_angle = EditorGUILayout.FloatField("End Angle", shot.end_angle);
            shot.shot_size = EditorGUILayout.FloatField("Shot Size", shot.shot_size);
            shot.bulletBehaviour.speed = EditorGUILayout.FloatField("Speed", shot.bulletBehaviour.speed);
            EditorGUILayout.Space();

            shot.start_delay = EditorGUILayout.FloatField("Start Delay", shot.start_delay);
            shot.shoot_delay = EditorGUILayout.FloatField("Shoot Delay", shot.shoot_delay);
            shot.is_infinite_shots = EditorGUILayout.Toggle("Infinite Shots?", shot.is_infinite_shots);
            if (!shot.is_infinite_shots)
                shot.shoot_count = EditorGUILayout.FloatField("Shoot Count", shot.shoot_count);

            EditorGUILayout.Space();
            shot.loop_shot = EditorGUILayout.Toggle("Loop Shot?", shot.loop_shot);
            if (shot.loop_shot)
            {
                shot.loop_delay = EditorGUILayout.FloatField("Loop Delay", shot.loop_delay);
            }
        }
    }
}

#endif
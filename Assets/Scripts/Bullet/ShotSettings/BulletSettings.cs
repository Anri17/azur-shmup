using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    public enum BulletType
    {
        LINEAR,
        DYNAMIC
    }

    [System.Serializable]
    public class BulletSpeedTransition
    {
        public float transitionDelay = 1f;
        public float targetSpeed = 40f;
        public float transitionDuration = 1f;
    }
    
    [RequireComponent(typeof(BulletSpawnerPosition))]
    public class BulletSettings : MonoBehaviour
    {
        public GameObject bulletPrefabTemplate;
        
        public BulletType bulletType;
        
        [FormerlySerializedAs("bulletSpawnPosition")] [FormerlySerializedAs("shotPosition")] public BulletSpawnerPosition bulletSpawnerPosition;
        
        // linear
        // angle
        public GameObject target;
        public float angleOffset;
        // speed
        public float speed;
        public BulletSpeedTransition[] bulletSpeedTransitions;

        private void Reset()
        {
            bulletSpawnerPosition = GetComponent<BulletSpawnerPosition>();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject.Core;
using UnityEngine;

namespace AzurProject.Bullet
{
    public enum BulletTargetType
    {
        NONE,
        PLAYER
    }
    
    [RequireComponent(typeof(BulletSpawnPosition))]
    public class BulletTarget : MonoBehaviour
    {
        public BulletSpawnPosition bulletSpawnPosition;

        private void Reset()
        {
            bulletSpawnPosition = GetComponent<BulletSpawnPosition>();
            if (bulletSpawnPosition == null) bulletSpawnPosition = gameObject.AddComponent<BulletSpawnPosition>();
        }

        public BulletTargetType type;

        public float angleOffset;

        public float GetAngle()
        {
            switch (type)
            {
                case BulletTargetType.NONE:
                    return angleOffset;
                case BulletTargetType.PLAYER:
                    var pos = bulletSpawnPosition.GetPosition();
                    return AzurProjectBase.GetAngle(bulletSpawnPosition.GetPosition(), 
                                                    GameManager.Instance.spawnedPlayer.transform)
                           + angleOffset;
                default:
                    return 0.0f;
            }
        }
    }
}

using UnityEngine;

namespace AzurShmup.Bullet
{
    public class ShotData : MonoBehaviour
    {
        public BulletGraphicType bulletGraphicType; //
        public BulletSpawnPositionType bulletSpawnPositionType;
        public BulletBehaviourType bulletBehaviourType;
        public ShotPatternType shotPatternType;
        public float bulletSpawnDelay; //

        [HideInInspector] public BulletSpawnPosition bulletSpawnPosition; // Defined in the Editor Script
        [HideInInspector] public BulletBehaviour bulletBehaviour; // Defined in the Editor Script
        [HideInInspector] public ShotPattern shotPattern; // Defined in the Editor Script

        public bool loopShot;
        public float loopDelay;
    }
}

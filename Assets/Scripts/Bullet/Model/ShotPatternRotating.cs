namespace AzurShmup.Bullet
{
    [System.Serializable]
    public struct ShotPatternRotating
    {
        public BulletGraphic bulletGraphicType;
        public BulletSpawnPosition bulletSpawnPosition;
        public BulletBehaviourBasicA bulletBehaviour;
        public float bulletSpawnDelay;


        public float start_delay; // how long to way before starting the shot pattern
        public float shoot_delay; // how long to way before each cluster is shot
        public float cluster_size; // number of bullets to be fired at once
        public float cluster_count; // how many times to fire a cluster

        public float angle_start;
        public float angle_end;

        public bool isRandom; // fire in a orderly manner or in random angle locations (confined between angle_start and angle_end)



        // isRandom = false;
        public float angle_change;

        // isCluster = true;


        // isRandom = true;
        //public float 

        public bool loopShot;
        public float loopDelay;
    }
}
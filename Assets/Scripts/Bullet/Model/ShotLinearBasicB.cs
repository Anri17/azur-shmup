namespace AzurShmup.Bullet
{
    [System.Serializable]
    public struct ShotLinearBasicB
    {
        public BulletGraphic bulletGraphic;
        public BulletSpawnPosition bulletSpawnPosition;
        public BulletBehaviourBasicB bulletBehaviour;
        public float bulletSpawnDelay;

        public float start_delay;
        public float shoot_delay;
        public bool is_infinite_shots;
        public float shoot_count; // irelevant is_infinite_shots is true

        public bool loop_shot;
        public float loop_delay;
    }
}
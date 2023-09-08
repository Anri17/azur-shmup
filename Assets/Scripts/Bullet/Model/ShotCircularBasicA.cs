namespace AzurShmup.Bullet
{
    [System.Serializable]
    public struct ShotCircularBasicA
    {
        public BulletGraphic bulletGraphic;
        public BulletSpawnPosition bulletSpawnPosition;
        public BulletBehaviourBasicA bulletBehaviour;
        public float bulletSpawnDelay;

        public float start_delay;
        public float shoot_delay;
        public bool is_infinite_shots;
        public float shoot_count; // irrelevant is_infinite_shots is true
        
        public bool is_random_directions; // fire in a orderly manner or in random angle locations (confined between angle_start and angle_end)
        public int shot_directions; // irrelevant if is_random is true | In how many directions to fire
        public float start_angle;
        public float end_angle;
        public float shot_size; // how many bullets to fire in a single loop

        public bool loop_shot;
        public float loop_delay;
    }
}
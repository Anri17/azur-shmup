namespace AzurShmup.Bullet
{
    [System.Serializable]
    public class ShotPatternLinear : ShotPattern
    {
        public float startDelay;
        public float loopDelay = 1.0f;
        public float bulletCount;
    }
}
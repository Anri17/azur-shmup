namespace AzurShmup.Bullet
{
    [System.Serializable]
    public struct Shot
    {
        public ShotType type;

        public ShotLinearBasicA linearBasicA;
        public ShotLinearBasicB linearBasicB;
        public ShotLinearAcceleratingA linearAcceleratingA;
        public ShotCircularBasicA circularBasicA;
    }
}
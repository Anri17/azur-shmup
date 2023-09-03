namespace AzurShmup.Bullet
{
    [System.Serializable]
    public struct BulletBehaviour
    {
        public BulletBehaviourType type;

        public BulletBehaviourBasicA basicA;
        public BulletBehaviourBasicB basicB;
        public BulletBehaviourAcceleratingA acceleratingA;
    }
}
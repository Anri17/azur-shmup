using System.Threading;

namespace AzurShmup.Bullet
{
    [System.Serializable]
    public class BulletBehaviour
    {
        public BulletBehaviourType type;

        public BulletBehaviourBasicA basicA;
        public BulletBehaviourBasicB basicB;
    }
}
using UnityEngine;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(BulletLifeCycle))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed = 10;
        [SerializeField] protected float angle = 0;
        [SerializeField] protected float delay = 0;

        public float Speed { get => speed; set => speed = value; }
        public float Angle { get => angle; set => angle = value; }
        public float Delay { get => delay; set => delay = value; }
    }
}

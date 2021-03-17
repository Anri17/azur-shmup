using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(BulletLifeCycle))]
    public class Bullet : MonoBehaviour
    {
        [FormerlySerializedAs("_speed")] [SerializeField] protected float speed = 10;
        [FormerlySerializedAs("_angle")] [SerializeField] protected float angle = 0;

        public float Speed { get => speed; set => speed = value; }
        public float Angle { get => angle; set => angle = value; }

        private void Update()
        {
            // Set the Angle
            transform.rotation = Quaternion.Euler(0, 0, Angle + 180);

            // Move the Bullet
            transform.Translate(Vector3.up * ((speed / 10) * Time.deltaTime), Space.Self);
        }
    }
}

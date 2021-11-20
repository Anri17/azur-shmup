using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(BulletLifeCycle))]
    public class Bullet : MonoBehaviour
    {
        public BulletType type;

        public Coroutine movementCoroutine;
        
        protected float _speed = 10;
        protected float _angle = 0;

        public float Speed { get => _speed; set => _speed = value; }

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                transform.rotation = Quaternion.Euler(0, 0, _angle);
            }
        }
        public bool IsPooled { get; set; } = true;

        public void RemoveFromPlayField()
        {
            transform.position = Vector3.zero;
            Angle = 0;
            Speed = 0;
            // I need to stop the coroutine in the shot manager because it was there where I started it in the first place.
            // If I don't do this then unity will throw an annoying error for no reason.
            ShotManager.Instance.StopCoroutine(movementCoroutine);
            BulletManager.Instance.AddBulletToPool(this);
        }
        
        public void SetupBullet(Vector2 pos, float speed, float angle, Coroutine movement)
        {
            transform.position = pos;
            Speed = speed;
            Angle = angle;
            movementCoroutine = movement;
            IsPooled = false;
        }
    }
}

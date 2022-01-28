using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(BulletLifeCycle))]
    public class Bullet : MonoBehaviour
    {
        public delegate IEnumerator BulletBehaviour(Bullet bullet);
        
        public BulletSprite type;

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
                transform.rotation = Quaternion.Euler(0, 0, _angle/2.0f); // _angle is being doubled, and I don't know why, so it is being divided by 2.0f
            }
        }
        public bool IsPooled { get; set; } = true;

        public void RemoveFromPlayField()
        {
            transform.position = Vector3.zero;
            Angle = 0;
            Speed = 0;
            // I need to stop the movement coroutine in the shot manager because it was there where I started it in the first place.
            // If I don't do this then unity will throw an annoying error for no reason.
            StopCoroutine(movementCoroutine);
            BulletManager.Instance.BulletPool_Add_Bullet(this);
        }
        
        /// <summary>
        /// Starts a bullet
        /// </summary>
        /// <param name="pos">The starting position of the bullet</param>
        /// <param name="speed">The starting movement sleep of the bullet</param>
        /// <param name="angle">The starting angle of the bullet</param>
        /// <param name="bulletBehaviour">How the bullet will behave during it's life time</param>
        public void SetupBullet(Vector2 pos, float speed, float angle, BulletBehaviour bulletBehaviour)
        {
            transform.position = pos;
            Speed = speed;
            Angle = angle;
            movementCoroutine = StartCoroutine(bulletBehaviour(this));
            IsPooled = false;
        }
    }
}

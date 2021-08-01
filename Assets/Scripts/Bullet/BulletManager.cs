using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using AzurProject.Bullet;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AzurProject.Bullet
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        public void CreateBullet(BulletSpawnerPosition bulletSpawnerPosition, BulletSettings bulletSettings)
        {
            
        }
        
        public Bullet CreateShotA1(Vector2 pos, float speed, float angle, GameObject graphic, float delay)
        {
            GameObject gameObject = Instantiate(graphic, pos, Quaternion.identity);

            Bullet bullet = gameObject.GetComponent<Bullet>();

            bullet.Speed = speed;
            bullet.Angle = angle;
            bullet.Delay = delay;

            return bullet;
        }

        public Bullet CreateShotA2(Vector2 pos, float speed, float angle, float accel, float maxSpeed,
            GameObject graphic,
            float delay)
        {
            GameObject gameObject = Instantiate(graphic, pos, Quaternion.identity);

            Bullet bullet = gameObject.GetComponent<Bullet>();

            return new Bullet();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using AzurProject.Bullet;
using UnityEngine;

namespace AzurProject
{
    public class PlayerStaticBullet : PlayerBullet
    {
        [SerializeField] private float _damage = 1.0f;

        public override float Damage { get; set; }

        private void Update()
        {
            // Move the Bullet
            transform.Translate(Vector3.up * (speed / 10) * Time.deltaTime, Space.Self);
        }

        private void Start()
        {
            Damage = _damage;
            Angle = 180;
        }
    }
}

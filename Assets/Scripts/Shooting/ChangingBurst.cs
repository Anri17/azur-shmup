using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Shooting
{
    public class ChangingBurst : Shot
    {
        [FormerlySerializedAs("_initialSpeed")]
        [Header("Changing Burst")]
        [Tooltip("The speed of the first bullet")]
        [SerializeField] private float initialSpeed = 10.0f;
        [FormerlySerializedAs("_finalSpeed")]
        [Tooltip("The speed of the last bullet")]
        [SerializeField] private float finalSpeed = 80.0f;
        [FormerlySerializedAs("_transitionSpeed")]
        [Tooltip("How fast the transition is")]
        [SerializeField] private float transitionSpeed = 10.0f;
        [FormerlySerializedAs("_bulletCount")]
        [Tooltip("How many bullets to fire")]
        [SerializeField] private int bulletCount;

        private void Awake()
        {
            speed = initialSpeed;
        }

        protected override IEnumerator ShootCoroutine()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(0.5f);
            }
        }

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindWithTag(target.tag).transform);
        }
    }
}

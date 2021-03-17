using System.Collections;
using UnityEngine;

namespace AzurProject.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Shot : MonoBehaviour
    {
        [Header("Base")]
        public GameObject bullet;
        public GameObject target;
        public float aimOffset;
        public float speed;

        protected GameObject BulletGameObject;
        protected Bullet.Bullet BulletScript;
        protected Coroutine FireCoroutine;
        protected bool IsInsidePlayField;

        private void Awake()
        {
            IsInsidePlayField = false;
        }

        protected abstract void Fire();

        protected abstract IEnumerator ShootCoroutine();
        
        protected void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea") && !IsInsidePlayField)
            {
                FireCoroutine = StartCoroutine(ShootCoroutine());
                IsInsidePlayField = true;
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                if (FireCoroutine != null)
                {
                    StopCoroutine(FireCoroutine);
                }
                IsInsidePlayField = false;
            }
        }
    }
}

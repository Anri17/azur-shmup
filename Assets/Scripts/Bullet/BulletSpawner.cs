using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BulletSpawnerPosition))]
    [RequireComponent(typeof(BulletSettings))]
    public class BulletSpawner : MonoBehaviour
    {
        protected BulletSettings BulletSettings;
        protected GameObject BulletGameObject;
        protected Coroutine fireCoroutine;
        protected bool IsInsidePlayField = false;

        protected BulletSpawnerPosition BulletSpawnerPosition;

        private void Reset()
        {
            // setup GameObject component
            tag = "Enemy";
            gameObject.layer = 8;
            // setup ShotDirection component
            BulletSettings = GetComponent<BulletSettings>();
            // setup Rigidbody2D component
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            // setup BoxCollider2D component
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size = new Vector2(0.1f, 0.1f);

            BulletSpawnerPosition = GetComponent<BulletSpawnerPosition>();
        }

        private void Awake()
        {
            if (BulletSettings == null)
            {
                BulletSettings = gameObject.AddComponent<BulletSettings>();
            }
            IsInsidePlayField = false;
        }

        protected virtual void SpawnBullet()
        {
            BulletManager.Instance.CreateBullet(BulletSpawnerPosition, BulletSettings);
        }

        protected virtual IEnumerator FireCoroutine()
        {
            yield return null;
        }
        
        protected void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea") && !IsInsidePlayField)
            {
                fireCoroutine = StartCoroutine(FireCoroutine());
                IsInsidePlayField = true;
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                if (fireCoroutine != null)
                {
                    StopCoroutine(fireCoroutine);
                }
                IsInsidePlayField = false;
            }
        }
    }
}

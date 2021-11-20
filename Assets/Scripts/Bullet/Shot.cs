using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BulletSettings))]
    public class Shot : MonoBehaviour
    {
        protected BulletManager _bulletManager;
        protected ShotManager _shotManager;

        public BulletSettings _bulletSettings;
        
        private bool _isInsidePlayField;
        private Coroutine _shotCoroutine;
        
        private void Awake()
        {
            _bulletManager = BulletManager.Instance;
            _shotManager = ShotManager.Instance;

            _isInsidePlayField = false;
        }

        private void Reset()
        {
            // setup GameObject component
            tag = "Enemy";
            gameObject.layer = 8;
            // setup BulletSettings component
            _bulletSettings = GetComponent<BulletSettings>();
            // setup Rigidbody2D component
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            // setup BoxCollider2D component
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size = new Vector2(0.1f, 0.1f);
        }

        protected virtual Coroutine Shoot()
        {
            Debug.Log("If you're seeing this message then that means that the Shoot() functions in the Shoot class of debug is being called. This should not be happening.");
            throw new Exception("Unexpected call to parent method.");
        }
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea") && !_isInsidePlayField)
            {
                _shotCoroutine = Shoot();
                _isInsidePlayField = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                if (_shotCoroutine != null)
                {
                    StopCoroutine(_shotCoroutine);
                }
                _isInsidePlayField = false;
            }
        }

        private void OnDestroy()
        {
            if (_shotCoroutine != null)
            {
                StopCoroutine(_shotCoroutine);
            }
            _isInsidePlayField = false;
        }
    }
}

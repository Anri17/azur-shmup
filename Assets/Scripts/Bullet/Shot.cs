using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    public enum PatternType
    {
        LINEAR,
        CONE,
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BulletSettings))]
    public class Shot : MonoBehaviour
    {
        public PatternType patternType;

        public float startDelay;
        public float loopDelay;
        public float bulletCount;

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
            switch (patternType)
            {
                case PatternType.LINEAR:
                {
                    return StartCoroutine(_shotManager.LinearPatternCoroutine(_bulletSettings, startDelay, loopDelay, bulletCount));
                } 
            }

            throw new Exception("Pattern not defined.");
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

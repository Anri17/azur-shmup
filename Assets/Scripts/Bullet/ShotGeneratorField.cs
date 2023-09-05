using System;
using UnityEngine;
using AzurShmup.Stage;
using System.Runtime.InteropServices.ComTypes;

namespace AzurShmup.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(ShotData))]
    public class ShotGeneratorField : MonoBehaviour
    {
        private ShotData _shotData;
        private bool _isInsidePlayField;
        private Coroutine _shotCoroutine;
        
        private void Awake()
        {
            _isInsidePlayField = false;
            _shotData = GetComponent<ShotData>();
        }

        private void Reset()
        {
            // setup GameObject component
            tag = "Enemy";
            gameObject.layer = 8;
            // setup Rigidbody2D component
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            // setup BoxCollider2D component
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size = new Vector2(0.1f, 0.1f);
            // Shot Data
            _shotData = gameObject.AddComponent<ShotData>();
        }
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea") && !_isInsidePlayField)
            {
                _isInsidePlayField = true;
                StartShot();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                StopShot();
                _isInsidePlayField = false;
            }
        }

        private void OnDestroy()
        {
            StopShot();
            _isInsidePlayField = false;
        }

        public void StopShot()
        {
            if (_shotCoroutine != null)
            {
                ShotManager.Instance.StopCoroutine(_shotCoroutine);
            }
        }

        public void StartShot()
        {
            _shotCoroutine = ShotManager.Instance.StartShot(_shotData);
        }
    }
}

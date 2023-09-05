using System;
using System.Collections;
using UnityEngine;

namespace AzurShmup.Bullet
{
    public class BulletLifeCycle : MonoBehaviour
    {
        private bool _isInsidePlayfield;
        private float bulletLifeCycle = 1.0f;
        private Bullet _parentBullet; 
        private Coroutine _lifeCycleCoroutine;

        private void Awake()
        {
            _parentBullet = GetComponent<Bullet>();
        }

        private void Start()
        {
            _isInsidePlayfield = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
             {
            if (collision.CompareTag("PlayArea") && _isInsidePlayfield)
            {
                _isInsidePlayfield = false;
                _lifeCycleCoroutine = StartCoroutine(BulletLifeCycleCoroutine(bulletLifeCycle));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayArea") && !_isInsidePlayfield)
            {
                _isInsidePlayfield = true;
                if (_lifeCycleCoroutine != null)
                {
                    StopCoroutine(_lifeCycleCoroutine);
                }
            }
        }

        private IEnumerator BulletLifeCycleCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _parentBullet.RemoveFromPlayField();
        }
    }
}

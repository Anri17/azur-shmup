using System;
using System.Collections;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class BulletLifeCycle : MonoBehaviour
    {
        private float bulletLifeCycle = 4.0f;
        private Bullet _parentBullet; 
        private Coroutine _lifeCycleCoroutine;

        private void Awake()
        {
            _parentBullet = GetComponent<Bullet>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea") && !_parentBullet.IsPooled)
            {
                _lifeCycleCoroutine = StartCoroutine(BulletLifeCycleCoroutine(bulletLifeCycle));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayArea") && !_parentBullet.IsPooled)
            {
                if (_lifeCycleCoroutine != null)
                    StopCoroutine(_lifeCycleCoroutine);
            }
        }

        private IEnumerator BulletLifeCycleCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _parentBullet.RemoveFromPlayField();
        }
    }
}

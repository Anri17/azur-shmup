using System.Collections;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class BulletLifeCycle : MonoBehaviour
    {
        private float bulletLifeCycle = 4.0f;
        
        private Coroutine _lifeCycleCoroutine;
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                _lifeCycleCoroutine = StartCoroutine(BulletLifeCycleCoroutine(bulletLifeCycle));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayArea"))
            {
                if (_lifeCycleCoroutine != null)
                    StopCoroutine(_lifeCycleCoroutine);
            }
        }

        private IEnumerator BulletLifeCycleCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}

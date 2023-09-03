using System.Collections;
using UnityEngine;

namespace AzurShmup
{
    public class LifeCycle : MonoBehaviour
    {
        [SerializeField] private float lifeCycle;

        private void Start()
        {
            StartCoroutine(LifeCycleCoroutine());
        }

        private IEnumerator LifeCycleCoroutine()
        {
            yield return new WaitForSeconds(lifeCycle);
            Destroy(gameObject);
        }
    }
}

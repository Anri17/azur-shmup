using System.Collections;
using UnityEngine;

namespace AzurProject.Shooting
{
    public class ShotInstantiate : MonoBehaviour
    {
        [SerializeField] private GameObject shot;
        [SerializeField] private float delay;


        private GameObject _spawnedSpell;

        private void Start()
        {
            StartCoroutine(SpawnSpellCoroutine());
        }

        private IEnumerator SpawnSpellCoroutine()
        {
            yield return new WaitForSeconds(delay);
            _spawnedSpell = Instantiate(shot, transform.position, Quaternion.identity);
        }

        private void OnDestroy()
        {
            Destroy(_spawnedSpell);
        }
    }
}

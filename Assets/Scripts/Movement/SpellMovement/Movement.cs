using System.Collections;
using UnityEngine;

namespace AzurProject.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Movement : MonoBehaviour, IMovement
    {
        protected Transform MainTransform;

        [HideInInspector] public Vector3 direction;

        private void Awake()
        {
            if (transform.parent != null)
            {
                MainTransform = transform.parent;
            }
            else
            {
                MainTransform = gameObject.transform;
            }
        }

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        public abstract IEnumerator MoveCoroutine();
        
        /*
        protected float GetAngle(Transform targetTransform)
        {
            // We give the player prefab, but we need the actual player, so we need to scan the playfield for it
            if (targetTransform != null && targetTransform.CompareTag("Player"))
            {
                targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            if (targetTransform != null)
            {
                Vector3 dir = targetTransform.position - transform.position;
                dir = targetTransform.InverseTransformDirection(dir);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

                return angle;
            }
            return 0f;
        }
        */
    }
}

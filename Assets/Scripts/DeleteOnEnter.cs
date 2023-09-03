using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    public class DeleteOnEnter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}

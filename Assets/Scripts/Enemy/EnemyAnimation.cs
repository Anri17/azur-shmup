using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurShmup
{
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator animator;

        private Vector2 lastPos;
        private Vector2 currentPos;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();

            lastPos = transform.position;
            currentPos = transform.position;
        }

        private void Update()
        {
            currentPos = transform.position;

            float xDirection = currentPos.x - lastPos.x;
            
            if (xDirection > 0)
            {
                AnimateRight();
            }
            else if (xDirection < 0)
            {
                AnimateLeft();
            }
            else
            {
                AnimateStill();
            }

            lastPos = currentPos;
        }

        private void AnimateLeft()
        {
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveLeft", true);
        }

        private void AnimateRight()
        {
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", true);
        }

        private void AnimateStill()
        {
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", false);
        }
    }
}

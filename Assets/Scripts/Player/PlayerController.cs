using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float normalSpeed = 12.0f;
        [SerializeField] float focusSpeed = 4.0f;
        [SerializeField] public Animator animatorController;

        private PlayerInput playerInput;

        public float Speed { get; set; }

        public bool canMove = false;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            animatorController.SetLayerWeight(1, 1f);
            canMove = true;
            Speed = normalSpeed;
        }

        void Update()
        {
            // set focus speed value
            if (playerInput.FocusInput)
            {
                Speed = focusSpeed;
                animatorController.SetBool("FocusMode", true);
            }
            else
            {
                Speed = normalSpeed;
                animatorController.SetBool("FocusMode", false);
            }

            if (playerInput.HorizontalInput || playerInput.VerticalInput)
            {
                Vector3 direction = GetDirection(playerInput.HorizontalInputValue, playerInput.VerticalInputValue);
                Move(direction, Speed);
            }
        }

        private void Move(Vector3 direction, float speed)
        {
            if (canMove)
            {
                Vector3 newPosition = transform.position + (direction * speed * Time.deltaTime);

                float spriteWidth = 1.2f;       // TODO: get the value automatically from the sprite image width
                float spriteHeight = 1.8f;      // TODO: get the value automatically from the sprite image height

                float leftLimit = GameManager.GAME_FIELD_BOTTOM_LEFT.x + spriteWidth / 2;
                float rightLimit = GameManager.GAME_FIELD_BOTTOM_RIGHT.x - spriteWidth / 2;
                float topLimit = GameManager.GAME_FIELD_TOP_RIGHT.y - spriteHeight / 2;
                float bottomLimit = GameManager.GAME_FIELD_BOTTOM_RIGHT.y + spriteHeight / 2;

                if (newPosition.x < leftLimit)   // block movement on the left limit of the play screen
                {
                    newPosition = new Vector3(leftLimit, newPosition.y, newPosition.z);
                }
                else if (newPosition.x > rightLimit)    // block movement on the right limit of the play screen
                {
                    newPosition = new Vector3(rightLimit, newPosition.y, newPosition.z);
                }

                if (newPosition.y < bottomLimit)    // block movement on the bottom limit of the play screen
                {
                    newPosition = new Vector3(newPosition.x, bottomLimit, newPosition.z);
                }
                else if (newPosition.y > topLimit)  // block movement on the top limit of the play screen
                {
                    newPosition = new Vector3(newPosition.x, topLimit, newPosition.z);
                }

                transform.position = newPosition;
            }
        }

        Vector3 GetDirection(float horizontal, float vertical)
        {
            if (horizontal != 0 && vertical != 0)
            {
                if (horizontal > 0)
                {
                    return new Vector3(Mathf.Cos(horizontal), Mathf.Sin(vertical), 0.0f);
                }
                if (horizontal < 0)
                {
                    return new Vector3(-Mathf.Cos(horizontal), Mathf.Sin(vertical), 0.0f);
                }
            }

            return new Vector3(horizontal, vertical, 0.0f);
        }
    }
}

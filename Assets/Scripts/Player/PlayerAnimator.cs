using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private PlayerInput _playerInput;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            if (_playerInput.HorizontalInputValue > 0)
            {
                _animator.SetBool("MoveLeft", false);
                _animator.SetBool("MoveRight", true);
            }
            else if (_playerInput.HorizontalInputValue < 0)
            {
                _animator.SetBool("MoveLeft", true);
                _animator.SetBool("MoveRight", false);
            }
            else
            {
                _animator.SetBool("MoveLeft", false);
                _animator.SetBool("MoveRight", false);
            }
        }
    }
}

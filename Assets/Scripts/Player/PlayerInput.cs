using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject.Core;
using UnityEngine;

namespace AzurProject
{
    public class PlayerInput : MonoBehaviour
    {
        public bool FireInput { get; private set; }
        public bool BombInput { get; private set; }
        public bool FocusInput { get; private set; }
        public bool HorizontalInput { get; private set; }
        public bool VerticalInput { get; private set; }
        public float HorizontalInputValue { get; private set; }
        public float VerticalInputValue { get; private set; }

        private InputManager _inputManager;

        private void Awake()
        {
            _inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_inputManager.Focus))
            {
                FocusInput = true;
            }
            if (Input.GetKeyUp(_inputManager.Focus))
            {
                FocusInput = false;
            }
            
            
            HorizontalInput = Input.GetKey(_inputManager.Left) || Input.GetKey(_inputManager.Right);
            VerticalInput = Input.GetKey(_inputManager.Up) || Input.GetKey(_inputManager.Down);
            
            HorizontalInputValue = Input.GetAxis("Horizontal");
            VerticalInputValue = Input.GetAxis("Vertical");
            
            if (HorizontalInput)
            {
                if (Input.GetKey(_inputManager.Left) && !Input.GetKey(_inputManager.Right))
                {
                    HorizontalInputValue = -1;
                }
                
                if (Input.GetKey(_inputManager.Right) && !Input.GetKey(_inputManager.Left))
                {
                    HorizontalInputValue = 1;
                }
            }
            else
            {
                HorizontalInputValue = 0;
            }

            if (VerticalInput)
            {
                if (Input.GetKey(_inputManager.Down) && !Input.GetKey(_inputManager.Up))
                {
                    VerticalInputValue = -1;
                }
                else if (Input.GetKey(_inputManager.Up) && !Input.GetKey(_inputManager.Down))
                {
                    VerticalInputValue = 1;
                }
            }
            else
            {
                VerticalInputValue = 0;
            }
            
            if (Input.GetKeyDown(_inputManager.Shoot))
            {
                FireInput = true;
            }
            if (Input.GetKeyUp(_inputManager.Shoot))
            {
                FireInput = false;
            }

            BombInput = Input.GetKey(_inputManager.Special);
        }
    }
}

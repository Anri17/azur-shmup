using System;
using System.Collections;
using System.Collections.Generic;
using AzurShmup.Core;
using UnityEngine;

namespace AzurShmup
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


        private void Update()
        {
            if (InputManager.GetKeyDown(InputManager.Focus))
            {
                FocusInput = true;
            }
            if (InputManager.GetKeyUp(InputManager.Focus))
            {
                FocusInput = false;
            }
            
            
            HorizontalInput = InputManager.GetKey(InputManager.Left) || InputManager.GetKey(InputManager.Right);
            VerticalInput = InputManager.GetKey(InputManager.Up) || InputManager.GetKey(InputManager.Down);
            
            HorizontalInputValue = Input.GetAxis("Horizontal");
            VerticalInputValue = Input.GetAxis("Vertical");
            
            if (HorizontalInput)
            {
                if (InputManager.GetKey(InputManager.Left) && !InputManager.GetKey(InputManager.Right))
                {
                    HorizontalInputValue = -1;
                }
                
                if (InputManager.GetKey(InputManager.Right) && !InputManager.GetKey(InputManager.Left))
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
                if (InputManager.GetKey(InputManager.Down) && !InputManager.GetKey(InputManager.Up))
                {
                    VerticalInputValue = -1;
                }
                else if (InputManager.GetKey(InputManager.Up) && !InputManager.GetKey(InputManager.Down))
                {
                    VerticalInputValue = 1;
                }
            }
            else
            {
                VerticalInputValue = 0;
            }
            
            if (InputManager.GetKeyDown(InputManager.Shoot))
            {
                FireInput = true;
            }
            if (InputManager.GetKeyUp(InputManager.Shoot))
            {
                FireInput = false;
            }

            BombInput = InputManager.GetKey(InputManager.Special);
        }
    }
}

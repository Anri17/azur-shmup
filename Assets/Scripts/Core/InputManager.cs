using System;
using UnityEngine;

namespace AzurProject.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public KeyCode Up { get; set; }
        public KeyCode Down { get; set; }
        public KeyCode Left { get; set; }
        public KeyCode Right { get; set; }
        public KeyCode Shoot { get; set; }
        public KeyCode Focus { get; set; }
        public KeyCode Special { get; set; }

        private void Awake()
        {
            MakeSingleton();
            
            Up = KeyCode.UpArrow;
            Down = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
            Shoot = KeyCode.Z;
            Focus = KeyCode.LeftShift;
            Special = KeyCode.X;
        }
        
        private void MakeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

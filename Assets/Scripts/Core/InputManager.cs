using System;
using UnityEngine;

namespace AzurShmup.Core
{
    public static class InputManager
    {
        public static KeyCode Up { get; set; }
        public static KeyCode Down { get; set; }
        public static KeyCode Left { get; set; }
        public static KeyCode Right { get; set; }
        public static KeyCode Shoot { get; set; }
        public static KeyCode Focus { get; set; }
        public static KeyCode Special { get; set; }
        public static KeyCode Return { get; set; }
        public static KeyCode Pause { get; set; }
        public static KeyCode DebugMode { get; set; }

        static InputManager()
        {
            Up = KeyCode.UpArrow;
            Down = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
            Shoot = KeyCode.Z;
            Focus = KeyCode.LeftShift;
            Special = KeyCode.X;
            Return = KeyCode.Return;
            Pause = KeyCode.Escape;
            DebugMode = KeyCode.Backslash;
        }

        public static bool GetKey(KeyCode key) => Input.GetKey(key);
        public static bool GetKeyDown(KeyCode key) => Input.GetKeyDown(key);
        public static bool GetKeyUp(KeyCode key) => Input.GetKeyUp(key);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    /// <summary>
    /// Call MakeSingleton() on Awake() of child class.
    /// </summary>
    /// <typeparam name="T">Same Type as Child Class.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {  
                _instance = null;
            }
        }

        /// <summary>
        /// Needs to be called on Awake() of child class
        /// </summary>
        public void MakeSingleton()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(this);
            }
        }
    }

    /// <summary>
    /// Call MakeSingleton() on Awake() of child class.
    /// </summary>
    /// <typeparam name="T">Same Type as Child Class.</typeparam>
    public class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        /// <summary>
        /// Needs to be called on Awake() of child class
        /// </summary>
        public void MakeSingleton() 
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}

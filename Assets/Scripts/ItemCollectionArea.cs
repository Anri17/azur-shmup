using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject
{
    public class ItemCollectionArea : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 90f;

        private GameManager _gameManager;
        private Player _player;

        public bool canSucc = true;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        private void CollectAllItems()
        {
            if (canSucc)
            {
                // TODO: items should be spawned and stored in a list, so we don't have to find them all the time.
                GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");

                for (int i = 0; i < collectables.Length; i++)
                {
                    Collectable collectable = collectables[i].GetComponent<Collectable>();
                    
                    collectable.MoveToPlayer(moveSpeed);
                }
            }
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CollectAllItems();
            }
        }
    }
}
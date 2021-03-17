using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject
{
    public class Collectable : MonoBehaviour
    {
        public int scoreWorth = 0;
        public float powerLevelWorth = 0f;
        public int bombsWorth = 0;
        public int livesWorth = 0;

        private bool moveTowardsTarget = false;
        private bool canMoveTowardsTarget = true;

        private float moveSpeed = 0f;
        private Vector3 target;
        private bool moveToPlayer = false;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (moveTowardsTarget && canMoveTowardsTarget)
            {
                if (moveToPlayer && GameObject.Find("ItemCollectionArea").GetComponent<ItemCollectionArea>().canSucc)
                {
                    scoreWorth = 10000;
                    Succ(gameManager.spawnedPlayer.transform.position, moveSpeed);
                }
                else
                {
                    Succ(target, moveSpeed);
                    if (transform.position == target)
                    {
                        moveTowardsTarget = false;
                    }
                }
            }
        }

        public void Move(Transform target, float moveSpeed, float timeToWait)
        {
            this.target = target.position;
            this.moveSpeed = moveSpeed;
            moveTowardsTarget = true;
            StartCoroutine(WaitTimeBeforeStoppingMovement(timeToWait));
        }

        public void Move(Vector3 target, float moveSpeed, float timeToWait)
        {
            this.target = target;
            this.moveSpeed = moveSpeed;
            moveTowardsTarget = true;
            StartCoroutine(WaitTimeBeforeStoppingMovement(timeToWait));
        }

        public void MoveToPlayer(float moveSpeed)
        {
            target = gameManager.spawnedPlayer.transform.position;
            moveToPlayer = true;
            this.moveSpeed = moveSpeed;
            moveTowardsTarget = true;
        }

        private void Succ(Vector3 target, float moveSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        private IEnumerator WaitTimeBeforeStoppingMovement(float timeToWait)
        {
            if (timeToWait == 0)
                yield return null;
            else
            {
                yield return new WaitForSeconds(timeToWait);
                moveTowardsTarget = false;
            }
        }

        // Stop being able to move towards target after exiting the playfield
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                canMoveTowardsTarget = false;
                LifeCycleCoroutine = StartCoroutine(BulletLifeCycleCoroutine(bulletLifeCycle));
            }
        }

        // For items that drop on the top of the screen outside of the playfield, when they enter back, enable the movement to the player
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayArea"))
            {
                canMoveTowardsTarget = true;
                if (LifeCycleCoroutine != null)
                    StopCoroutine(LifeCycleCoroutine);
            }
        }

        private float bulletLifeCycle = 3f;
        private Coroutine LifeCycleCoroutine;

        private IEnumerator BulletLifeCycleCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}

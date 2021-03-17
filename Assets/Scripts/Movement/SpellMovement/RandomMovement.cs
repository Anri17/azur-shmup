using System.Collections;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject.Movement
{
    public class RandomMovement : Movement
    {
        public float delay = 1f;

        public override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                Vector3 point = GetRandomPoint();
                StartCoroutine(LerpMoveCoroutine(point, 1f));
                yield return new WaitForSeconds(2f);
            }
        }

        public Vector3 GetRandomPoint()
        {
            float spriteWidthSize;
            float spriteHeightSize;
            if (MainTransform.GetComponent<SpriteRenderer>() != null)
            {
                spriteWidthSize = MainTransform.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
                spriteHeightSize = MainTransform.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            }
            else
            {
                spriteWidthSize = 1;
                spriteHeightSize = 1;
            }

            float bottomYOffset = 4f;
            float randX = UnityEngine.Random.Range(GameManager.GAME_FIELD_TOP_LEFT.x + spriteWidthSize, GameManager.GAME_FIELD_TOP_RIGHT.x - spriteWidthSize);
            float randY = UnityEngine.Random.Range(GameManager.GAME_FIELD_CENTER.y + bottomYOffset + spriteHeightSize, GameManager.GAME_FIELD_TOP_LEFT.y - spriteHeightSize);

            return new Vector3(randX, randY, 0);
        }
        
        private IEnumerator LerpMoveCoroutine(Vector3 destination, float duration)
        {
            Vector3 currentPos = MainTransform.position;
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                MainTransform.position = Vector3.Lerp(currentPos, destination, t);
                yield return null;
            }
        }
    }
}

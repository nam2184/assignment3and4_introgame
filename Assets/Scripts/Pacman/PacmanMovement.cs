using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Speed of movement
    public float moveDuration = 0.5f; // Time taken to move from one tile to the next
    public Vector2 gridSize = new Vector2(1, 1);

    public AudioSource soundEffect; // First audio source
    public GameObject item;
    public bool keepMoving = true;
    private Tweener tweener;
    public Animator animator;
    
     void Start()
    {
        tweener = GetComponent<Tweener>();
        StartCoroutine(MoveWithDelay());
    }

    IEnumerator MoveWithDelay()
    {
        // Move right for 5 tiles
        while (keepMoving) {
          for (int i = 1; i <= 5; i++)
          {
              animator.SetInteger("Direction", 3); // Transition to right
              TweenAdd(Vector2.right, moveDuration);

              // Wait for 1 second before moving again
              yield return new WaitForSeconds(1f);
          }

          // Move down for 4 tiles
          for (int i = 1; i <= 4; i++)
          {
              animator.SetInteger("Direction", 1); // Transition to down
              TweenAdd(Vector2.down, moveDuration);

              yield return new WaitForSeconds(1f);
          }

          // Move left for 5 tiles
          for (int i = 1; i <= 5; i++)
          {
              animator.SetInteger("Direction", 2); // Transition to left
              TweenAdd(Vector2.left, moveDuration);

              yield return new WaitForSeconds(1f);
          }

          // Move up for 4 tiles
          for (int i = 1; i <= 4; i++)
          {
              animator.SetInteger("Direction", 0); // Transition to up
              TweenAdd(Vector2.up, moveDuration);

              yield return new WaitForSeconds(1f);
          }
        }
    }

    private void TweenAdd(Vector2 direction, float duration)
    {
        Vector2 startPosition = item.transform.position; // Current position
        Vector2 endPosition = startPosition + direction * gridSize; // Target position in grid units
        soundEffect.Play();
        tweener.AddTween(item.transform, startPosition, endPosition, duration);    
    }
}

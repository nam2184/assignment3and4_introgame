using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNPCMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 gridSize = new Vector2(1, 1);
    private Vector2 targetPosition;
    private bool canMove = true;
    private Animator animator;
    private Vector2 currentDirection;

    void Start()
    {
        targetPosition = SnapToGrid(transform.position);
        animator = GetComponent<Animator>(); // Get the Animator component attached to the NPC GameObject

        // Start the NPC with a random initial direction
        ChooseRandomDirection();
    }

    void Update()
    {
        // Handle autonomous tile movement
        if (canMove)
        {
            MoveNPC();
        }
    }

    void MoveNPC()
    {
        // Keep moving in the current direction
        targetPosition += currentDirection * gridSize;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If we reach the target position, check for collisions or keep moving
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = SnapToGrid(transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // If NPC hits a wall, choose a new random direction
            ChooseRandomDirection();
        }
    }

    Vector2 SnapToGrid(Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x / gridSize.x) * gridSize.x,
                           Mathf.Round(position.y / gridSize.y) * gridSize.y);
    }

    void ChooseRandomDirection()
    {
        Vector2 currentPosition = SnapToGrid(transform.position);

        // Possible directions to move: up, down, left, right
        Vector2[] directions = {
            Vector2.up, 
            Vector2.down, 
            Vector2.left, 
            Vector2.right
        };

        // Filter out directions that lead to a wall
        System.Collections.Generic.List<Vector2> validDirections = new System.Collections.Generic.List<Vector2>();
        foreach (Vector2 dir in directions)
        {
            Vector2 checkPos = currentPosition + dir * gridSize;
            Collider2D hitCollider = Physics2D.OverlapBox(checkPos, gridSize / 2, 0);

            // If there's no wall in that direction, consider it valid
            if (hitCollider == null || !hitCollider.CompareTag("Wall"))
            {
                validDirections.Add(dir);
            }
        }

        // Choose a random valid direction
        if (validDirections.Count > 0)
        {
            currentDirection = validDirections[Random.Range(0, validDirections.Count)];

            // Update Animator direction based on chosen movement direction
            if (currentDirection == Vector2.up)
            {
                SetDirection(1); // Up
            }
            else if (currentDirection == Vector2.down)
            {
                SetDirection(2); // Down
            }
            else if (currentDirection == Vector2.left)
            {
                SetDirection(3); // Left
            }
            else if (currentDirection == Vector2.right)
            {
                SetDirection(4); // Right
            }
        }
    }

    // Function to set Animator's direction parameter
    void SetDirection(int direction)
    {
        animator.SetInteger("Direction", direction);
    }
}


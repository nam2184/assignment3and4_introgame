using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public GameObject item; // Reference to the item to be moved
    public float moveDuration = 0.1f; // Speed of PacStudent's movement
    private Tweener tweener; // Reference to the Tweener
    private Vector2 targetPosition; // Target position to move to
    private string lastInput; // Last key input
    private string currentInput; // Current input being processed
    private Vector2 currentGridPosition; // Current grid position of PacStudent
    public Vector2 gridSize = new Vector2(1, 1);
    public Animator animator;
    public Tilemap tilemap;
    public AudioSource soundEffect; // First audio source
    public AudioSource pelletEat; // Second audio source
    public GameObject dustEffectPrefab; 
    
    private void Start()
    {
        tweener = gameObject.AddComponent<Tweener>(); // Add the Tweener component
        currentGridPosition = item.transform.position; 
        targetPosition = item.transform.position;
        StartCoroutine(MovePacStudent(lastInput));
    }

    private void Update()
    {
        GatherInput(); // Always gather input in each frame
    }

    private void GatherInput()
    {
        // Store the last input key
        if (Input.GetKeyDown(KeyCode.W)) currentInput = "Up";
        else if (Input.GetKeyDown(KeyCode.A)) currentInput = "Left";
        else if (Input.GetKeyDown(KeyCode.S)) currentInput = "Down";
        else if (Input.GetKeyDown(KeyCode.D)) currentInput = "Right";
    }    

    IEnumerator MovePacStudent(string direction)
    {     
      while (true) {
          Vector2 startPosition = currentGridPosition; 
          Vector2 endPosition = startPosition + (GetDirectionVector(currentInput) * gridSize); // Calculate target position
          Vector2 endPosition2 = startPosition + (GetDirectionVector(lastInput) * gridSize); // Calculate target position
          if (IsWalkable(endPosition))
          {
              lastInput = currentInput;
              animator.SetInteger("Direction", GetIntDirection(lastInput));
              Move(startPosition, endPosition);
              currentGridPosition = endPosition; // Update current grid position
              yield return new WaitForSeconds(0.1f); // Adjust the delay time here
          } else if (IsWalkable(endPosition2)){
              endPosition = startPosition + (GetDirectionVector(lastInput) * gridSize); // Calculate target position
              Move(startPosition, endPosition2);
              currentGridPosition = endPosition2; // Update current grid position
              yield return new WaitForSeconds(0.1f); // Adjust the delay time here
          } else {
              yield return new WaitForSeconds(0.0f); // Adjust the delay time here
          }
          yield return new WaitForSeconds(0.0f); // Adjust the delay time here
      }
    }
    
    private void Move(Vector2 startPosition, Vector2 endPosition)
    {
        PlayMovementAudio(IsPellet(endPosition)); // Play movement audio
        tweener.AddTween(item.transform, startPosition, endPosition, moveDuration);
        CreateDustEffect(endPosition);
    }
   
    
    private bool IsWalkable(Vector2 gridPosition)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(gridPosition); // Convert to Tilemap grid coordinates
        TileBase tile = tilemap.GetTile(tilePosition); // Get the tile at the position

        // If there’s no tile, it's walkable
        if (tile == null) return true;

        // Check the tile name; return false if it's an unwalkable tile
        string tileName = tile.name;
        if (tileName == "1" || tileName == "2" || tileName == "3" || tileName == "4" || tileName == "7")
        {
            return false;
        }

        return true; // Walkable if not in the unwalkable tile set
    }

    private bool IsPellet(Vector2 gridPosition)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(gridPosition); // Convert to Tilemap grid coordinates
        TileBase tile = tilemap.GetTile(tilePosition); // Get the tile at the position

        if (tile == null) return false;

        string tileName = tile.name;
        if (tileName == "5")
        {
            tilemap.SetTile(tilePosition, null);
            return true;
        }

        return false;
    }
    private Vector2 GetDirectionVector(string direction)
    {
        switch (direction)
        {
            case "Up":
                return Vector2.up;
            case "Left":
                return Vector2.left;
            case "Down":
                return Vector2.down;
            case "Right":
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
    
    private int GetIntDirection(string direction)
    {
        switch (direction)
        {
            case "Up":
                return 0;
            case "Left":
                return 2;
            case "Down":
                return 1;
            case "Right":
                return 3;
            default:
                return 0;
        }
    }

    private void PlayMovementAudio(bool pellet)
    {   
        if (pellet) {
          pelletEat.Play();
        }
        soundEffect.Play();
    }
    
    private void CreateDustEffect(Vector3 position)
    {
        // Instantiate dust effect at the given position
        GameObject dustEffect = Instantiate(dustEffectPrefab, position, Quaternion.identity);
        
        // Destroy the dust effect after it finishes playing
        ParticleSystem ps = dustEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(dustEffect, ps.main.duration); // Destroy after duration
        }
        else
        {
            Destroy(dustEffect, 1f); // Fallback duration if no particle system is found
        }
    }
    
    
}


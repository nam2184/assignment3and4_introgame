using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab; 
    public float spawnInterval = 10f; 
    public float moveSpeed = 5f; 
    public Camera mainCamera;
    
    private void Start()
    {
        StartCoroutine(SpawnCherryRoutine());
    }

    private IEnumerator SpawnCherryRoutine()
    {
        while (true)
        {
            SpawnCherry();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCherry()
    {
        int side = GetRandomSide();
        Vector2 spawnPosition = GetRandomSpawnPositionOutsideCamera(side);
        GameObject cherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(MoveCherry(cherry, side));
    }

    private int GetRandomSide() 
    {
        int side = Random.Range(0, 4); // 0 = Top, 1 = Bottom, 2 = Left, 3 = Right;
        return side;
    }

    private Vector2 GetRandomSpawnPositionOutsideCamera(int side)
    {
        float cameraHeight = mainCamera.orthographicSize * 2; 
        float cameraWidth = cameraHeight * mainCamera.aspect; 

        // Get the camera position in world coordinates
        Vector2 cameraPosition = mainCamera.transform.position;

        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector2(cameraPosition.x, cameraPosition.y + cameraHeight / 2 + 1); // Centered horizontally, just above the top edge
                break;
            case 1: // Bottom
                spawnPosition = new Vector2(cameraPosition.x, cameraPosition.y - cameraHeight / 2 - 1); // Centered horizontally, just below the bottom edge
                break;
            case 2: // Left
                spawnPosition = new Vector2(cameraPosition.x - cameraWidth / 2 - 1, cameraPosition.y); // Centered vertically, just left of the left edge
                break;
            case 3: // Right
                spawnPosition = new Vector2(cameraPosition.x + cameraWidth / 2 + 1, cameraPosition.y); // Centered vertically, just right of the right edge
                break;
        }

        return spawnPosition;
    }


    private IEnumerator MoveCherry(GameObject cherry, int side)
    {
        Vector2 startPosition = cherry.transform.position;
        float cameraHeight = mainCamera.orthographicSize * 2; 
        float cameraWidth = cameraHeight * mainCamera.aspect; 

        Vector2 cameraPosition = mainCamera.transform.position;

        Vector2 endPosition;
        if (side == 0) // Spawned at the top
        {
            endPosition = new Vector2(cameraPosition.x, cameraPosition.y - cameraHeight / 2 - 1); // Move down
        }
        else if (side == 1) // Spawned at the bottom
        {
            endPosition = new Vector2(cameraPosition.x, cameraPosition.y + cameraHeight / 2 + 1); // Move up
        }
        else if (side == 2) // Spawned on the left side
        {
            endPosition = new Vector2(cameraPosition.x + cameraWidth / 2 + 1, cameraPosition.y); // Move to the right
        }
        else // Spawned on the right side
        {
            endPosition = new Vector2(cameraPosition.x - cameraWidth / 2 - 1, cameraPosition.y); // Move to the left
        }

        float journey = 0f;
        float duration = Vector2.Distance(startPosition, endPosition) / moveSpeed; 

        while (journey < duration && cherry != null)
        {
            journey += Time.deltaTime;
            cherry.transform.position = Vector2.Lerp(startPosition, endPosition, journey / duration); // Move cherry over time
            yield return null; // Wait for the next frame
        }
        if (cherry != null) {
          Destroy(cherry); 
        }
    }
}


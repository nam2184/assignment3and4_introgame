using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outsideCorner;   
    public GameObject outsideWall;        
    public GameObject insideCorner;       
    public GameObject insideWall;     
    public GameObject standardPellet;     
    public GameObject powerPellet;     
    public GameObject tJunction;       
    public GameObject pacStudent;
    
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    public float cellSize = 1.0f;
    public Vector3 gridOffset = new Vector3(-1f, 0, 0);
    
    void Start()
    {
        GenerateLevel();
    }

 
    void GenerateLevel()
    {
      GameObject previousLevel = GameObject.Find("Level 01");
      if (previousLevel != null)
      {
          Destroy(previousLevel);
      }
   
      Vector3 pacStudentPosition = pacStudent.transform.position;
      pacStudentPosition.z = 0;    

      int mapHeight = levelMap.GetLength(0);

      for (int y = 0; y < mapHeight; y++)
      {
          for (int x = 0; x < levelMap.GetLength(1); x++)
          {
              Vector3 originalPosition = new Vector3(x * cellSize, (y * -cellSize) + cellSize, 0) + pacStudentPosition + gridOffset;
              GeneratePiece(levelMap[y, x], originalPosition, $"Piece_{y}_{x}");
          }
      }
    }

    void GeneratePiece(int tileType, Vector3 position, string name)
    {
      GameObject toInstantiate = null;

      switch (tileType)
      {
          case 1: toInstantiate = outsideCorner; break;
          case 2: toInstantiate = outsideWall; break;
          case 3: toInstantiate = insideCorner; break;
          case 4: toInstantiate = insideWall; break;
          case 5: toInstantiate = standardPellet; break;
          case 6: toInstantiate = powerPellet; break;
          case 7: toInstantiate = tJunction; break;
          default: return; // Skip empty spots (0)
      }

      if (toInstantiate != null)
      {
          GameObject instance = Instantiate(toInstantiate, position, Quaternion.identity);
          instance.name = name;
      }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCollision : MonoBehaviour
{
    void OnTriggerExit(Collider collision)
    {
      Debug.Log("Collision detected with: " + collision.gameObject.name);
      if (collision.gameObject.CompareTag("Pacstudent"))
      {
        Destroy(gameObject); // Destroy Cherry on collision
      }
    }

}

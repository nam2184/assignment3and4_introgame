using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // Reset to Idle
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetInteger("Direction", 4); // Set to Idle state
        }

        // Check for movement input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetInteger("Direction", 0); // Transition to Up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("Direction", 1); // Transition to Down
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetInteger("Direction", 2); // Transition to Left
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("Direction", 3); // Transition to Right
        }
    }
}

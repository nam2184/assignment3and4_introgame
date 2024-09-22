using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : StateMachineBehaviour
{
    public float targetRotationY = 0f; // Rotation angle on the Y-axis
    public float rotationSpeed = 300f; // Speed of rotation in degrees per second

    private Transform characterTransform;

    // Called when the animator enters the state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the transform of the character
        characterTransform = animator.gameObject.transform;

        // Start a coroutine to rotate the character smoothly
        animator.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(RotateCharacter());
    }

    private System.Collections.IEnumerator RotateCharacter()
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetRotationY);

        // Rotate the character smoothly towards the target rotation
        while (Quaternion.Angle(characterTransform.rotation, targetRotation) > 0.1f)
        {
            characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Called on every frame while the state is being evaluated
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

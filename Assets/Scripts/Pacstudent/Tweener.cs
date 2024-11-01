using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween currentTween;

    public void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        currentTween = newTween;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTween != null)
        {
            float distanceToEnd = Vector2.Distance(currentTween.Target.position, currentTween.EndPos);

            if (distanceToEnd > 0.1f)
            {
                // Tween in progress, calculate Lerp value
                float elapsedTime = Time.time - currentTween.StartTime;
                float t = elapsedTime / currentTween.Duration;

                // Move the object towards the target grid position
                Vector2 newPos = Vector2.Lerp(currentTween.StartPos, currentTween.EndPos, t);
                currentTween.Target.position = newPos;
            }
            else
            {
                // Snap to the final position
                currentTween.Target.position = currentTween.EndPos;
                currentTween = null; // Tween is finished
            }
        }
    }
}


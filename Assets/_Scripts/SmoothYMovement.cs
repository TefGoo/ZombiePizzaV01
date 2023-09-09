using UnityEngine;
using UnityEngine.UI;

public class SmoothYMovement : MonoBehaviour
{
    public RectTransform targetTransform; // The RectTransform of the UI element you want to move.
    public float targetY = 200.0f;        // The target Y position you want to move to.
    public float speed = 2.0f;             // The speed at which the UI element moves.

    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            // Calculate the new Y position using Lerp for smooth movement.
            float newY = Mathf.Lerp(targetTransform.anchoredPosition.y, targetY, Time.deltaTime * speed);

            // Update the anchored position of the UI element with the new Y position.
            targetTransform.anchoredPosition = new Vector2(targetTransform.anchoredPosition.x, newY);

            // Check if the UI element is close enough to the target Y position.
            if (Mathf.Approximately(targetTransform.anchoredPosition.y, targetY))
            {
                isMoving = false; // Stop moving once you've reached the target.
            }
        }
    }

    public void StartMovingToTarget()
    {
        isMoving = true;
    }
}

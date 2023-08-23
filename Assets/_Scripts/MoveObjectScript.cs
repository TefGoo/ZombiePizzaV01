using UnityEngine;

public class MoveObjectScript : MonoBehaviour
{
    public Transform targetPosition; // Position to move towards
    public float moveSpeed = 5.0f;    // Speed of movement

    private void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
}

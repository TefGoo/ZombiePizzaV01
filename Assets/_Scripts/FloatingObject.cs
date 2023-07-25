using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.5f;  // Adjust this to control the height of floating.
    public float floatSpeed = 1.0f;      // Adjust this to control the speed of floating.

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Calculate the new vertical position using a sine wave.
        float newY = startPos.y + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);

        // Update the object's position.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

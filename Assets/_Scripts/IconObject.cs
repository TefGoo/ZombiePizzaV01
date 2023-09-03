using UnityEngine;

public class IconObject : MonoBehaviour
{
    public float floatAmplitudeZ = 0.1f;  // Adjust this to control the height of movement along the Z-axis.
    public float floatSpeedZ = 0.5f;      // Adjust this to control the speed of movement along the Z-axis.

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Calculate the new Z position using a sine wave.
        float newZ = startPos.z + floatAmplitudeZ * Mathf.Sin(Time.time * floatSpeedZ);

        // Update the object's position, only modifying the Z component.
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}

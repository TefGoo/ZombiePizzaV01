using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale value
    public float maxScale = 1.5f; // Maximum scale value
    public float scaleSpeed = 1.0f; // Speed of the scaling

    private float targetScale;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        targetScale = maxScale;
    }

    private void Update()
    {
        // Interpolate between the current scale and the target scale
        Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale * initialScale, Time.deltaTime * scaleSpeed);

        // Apply the new scale to the sprite
        transform.localScale = newScale;

        // Check if the scale is close to the target scale, then swap the target
        if (Vector3.Distance(transform.localScale, targetScale * initialScale) < 0.01f)
        {
            if (targetScale == maxScale)
                targetScale = minScale;
            else
                targetScale = maxScale;
        }
    }
}

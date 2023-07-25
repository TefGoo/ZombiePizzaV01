using UnityEngine;

public class CameraMoveOnTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public float enteringFOV = 60f; // FOV when entering the building
    public float exitingFOV = 90f;  // FOV when exiting the building

    private bool isInsideBuilding = false;

    private void Start()
    {
        // Check if the Cinemachine Virtual Camera reference is assigned
        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera reference is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInsideBuilding)
        {
            isInsideBuilding = true;
            UpdateFOV(enteringFOV);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInsideBuilding)
        {
            isInsideBuilding = false;
            UpdateFOV(exitingFOV);
        }
    }

    private void UpdateFOV(float newFOV)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.m_Lens.FieldOfView = newFOV;
        }
    }
}

using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float shootForce = 10f;
    public float shootDelay = 0.6f; // Delay in seconds
    public AudioClip gunAttackSound; // Reference to the gunshot sound effect

    public ParticleSystem muzzleFlash; // Reference to the muzzle flash particle system

    private bool canShoot = true; // Flag to track if shooting is allowed
    private AudioSource audioSource; // AudioSource component

    private CameraShake cameraShake; // Reference to the CameraShake script
    public float cameraShakeDuration = 0.2f;  // Duration of camera shake
    public float cameraShakeIntensity = 0.1f;  // Intensity of camera shake

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void OnPlayerAttack()
    {
        if (canShoot && bulletPrefab != null && spawnPoint != null)
        {
            for (int i = 0; i < 3; i++)
            {
                // Instantiate a new bullet
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

                // Calculate spread angle for shotgun effect
                float spreadAngle = -15f + i * 15f;

                // Apply spread rotation to the bullet's direction
                Vector3 spreadDirection = Quaternion.Euler(0f, spreadAngle, 0f) * spawnPoint.forward;

                // Get the Rigidbody component of the bullet
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                if (bulletRigidbody != null)
                {
                    // Apply force to shoot the bullet with spread direction
                    bulletRigidbody.velocity = spreadDirection * shootForce;
                }
            }

            // Play the gunshot sound effect
            audioSource.PlayOneShot(gunAttackSound);

            // Trigger camera shake
            cameraShake.ShakeCamera(cameraShakeDuration, cameraShakeIntensity);

            // Play the muzzle flash particle system
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            // Set a delay before shooting again
            StartCoroutine(ShootDelayCoroutine());
        }
    }

    private System.Collections.IEnumerator ShootDelayCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}

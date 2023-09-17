using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float shootForce = 10f;
    public float shootDelay = 0.6f;
    public AudioClip gunAttackSound;
    public ParticleSystem muzzleFlash;

    private bool canShoot = true;
    private AudioSource audioSource;
    private AmmoUI ammoUI;
    private AmmoManager ammoManager; // Reference to the AmmoManager

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammoUI = FindObjectOfType<AmmoUI>();
        ammoManager = FindObjectOfType<AmmoManager>(); // Find the AmmoManager component

        // Update the AmmoUI to display the initial ammo count
        if (ammoUI != null && ammoManager != null)
        {
            ammoUI.UpdateAmmo(ammoManager.GetCurrentAmmo());
        }
    }

    public void OnPlayerAttack()
    {
        if (canShoot && ammoManager != null && ammoManager.CanShoot())
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                float spreadAngle = -15f + i * 15f;
                Vector3 spreadDirection = Quaternion.Euler(0f, spreadAngle, 0f) * spawnPoint.forward;
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = spreadDirection * shootForce;
                }
            }

            audioSource.PlayOneShot(gunAttackSound);

            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            // Decrement the ammo count when shooting
            ammoManager.Shoot();

            // Update the AmmoUI to reflect the new remaining shots
            if (ammoUI != null)
            {
                ammoUI.UpdateAmmo(ammoManager.GetCurrentAmmo());
            }

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

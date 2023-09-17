using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 5; // Maximum ammo capacity
    private int currentAmmo; // Current ammo count

    // Initialize ammo count
    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Method to check if the player has enough ammo to shoot
    public bool CanShoot()
    {
        return currentAmmo > 0;
    }

    // Method to decrement the ammo count when the player shoots
    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }

    // Method to buy a new round of bullets
    public void BuyAmmo()
    {
        currentAmmo = maxAmmo;
    }

    // Method to get the current ammo count
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}

using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public GameObject weaponPrefab;
    public int weaponCost = 50;

    private bool isWeaponBought = false;
    private GameObject spawnedWeapon;
    private ScoreManager scoreManager;
    private Transform playerTransform;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        playerTransform = FindObjectOfType<Player>().transform;
    }

    public void BuyWeapon()
    {
        if (!isWeaponBought && scoreManager.Score >= weaponCost)
        {
            // Deduct the cost from the player's money
            scoreManager.AddPoints(-weaponCost);

            // Spawn the weapon at the shop's position
            spawnedWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);
            spawnedWeapon.GetComponent<Rigidbody>().isKinematic = true;
            spawnedWeapon.GetComponent<BoxCollider>().enabled = false;

            // Mark the weapon as bought
            isWeaponBought = true;

            // Parent the weapon to the player's transform to follow the player
            spawnedWeapon.transform.SetParent(playerTransform);

            Debug.Log("Weapon bought!");
        }
        else if (isWeaponBought)
        {
            Debug.Log("Weapon already bought!");
        }
        else
        {
            Debug.Log("Not enough money to buy the weapon!");
        }
    }

    public int GetWeaponCost()
    {
        return weaponCost;
    }

    public bool IsWeaponBought()
    {
        return isWeaponBought;
    }
}

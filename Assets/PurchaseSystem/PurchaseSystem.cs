using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _shotgunPrefab;

    public void BuyShotgun(Transform spawnLocation)
    {
        Instantiate(_shotgunPrefab,
            spawnLocation.position,
            spawnLocation.rotation);
        // spawn shotgun in world
        // play shotgun spawn sounds/particles/whatever?
    }

    public void BuySword(Transform spawnLocation)
    {
        Debug.Log("Buy Sword!");
    }
}

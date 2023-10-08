using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter, IInteractable
{

    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjects plateKitchenObjects;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnPlateCount;
    private int spawnPlateCountMax = 4;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (spawnPlateCount < spawnPlateCountMax)
            {
                spawnPlateCount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
                
            
        }
    }


    public void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is not holding anything
            if (spawnPlateCount > 0)
            {
                //plates available
                spawnPlateCount--;

                KitchenObjectManager.SpawnKitchenObject(plateKitchenObjects, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void KitchenAction(Player player)
    {
        //nothing
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

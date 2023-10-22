using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter, IInteractable
{
    [SerializeField] private KitchenObjects plateKitchenObjects;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    //controls how often plate is spawned
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    //how many plates spawned
    private int spawnPlateCount;
    private int spawnPlateCountMax = 4;


    private void Update()
    {
        //starts timer 
        spawnPlateTimer += Time.deltaTime;

        //basically every 4 secs spawn more plates & make sure less than 4
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

    //when player presses 'E' - make sure theyre not holding anything - give them plate (decrease spawned plates so more can spawn)
    public void InteractPrimary(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is not holding anything
            if (spawnPlateCount > 0)
            {
                //plates available
                spawnPlateCount--;

                KitchenObjectManager.SpawnKitchenObject(plateKitchenObjects, player);

                //updates visual
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void InteractSecondary(Player player)
    {
        //nothing
    }

    //usual position to make sure player within range
    public Transform GetTransform()
    {
        return transform;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter, IInteractable
{

    public static event EventHandler OnAnyObjectTrashed;
    
    //reset static events - keep previosu data
    public new static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    //if player has object - trash it - destroy object & sound
    public void InteractPrimary(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjectManager().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void InteractSecondary(Player player)
    {
        //no other specific action
    }

    //usual - position to check player within bounds to interact
    public Transform GetTransform()
    {
        return transform;
    }
}

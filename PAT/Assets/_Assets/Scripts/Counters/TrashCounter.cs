using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter, IInteractable
{

    public static event EventHandler OnAnyObjectTrashed;

    public new static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

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

    public Transform GetTransform()
    {
        return transform;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;

    public static void ResetStaticData()
    {
        OnAnyObjectPlaced = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObjectManager kitchenObjectManager;

    public Transform GetKitchenObjectFollowTransform()
    {
        //gets counter top point for attached counter
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager)
    {
        this.kitchenObjectManager = kitchenObjectManager;

        if (kitchenObjectManager != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObjectManager GetKitchenObjectManager()
    {
        return kitchenObjectManager;
    }

    public void ClearKitchenObject()
    {
        kitchenObjectManager = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjectManager != null;
    }
}

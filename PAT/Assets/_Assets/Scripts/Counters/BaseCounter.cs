using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{



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

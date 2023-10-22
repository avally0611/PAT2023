using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObjectManager kitchenObjectManager;

    //this resets the event so it doesnt contain previous data as its static (static events dont lose their data)
    public static void ResetStaticData()
    {
        OnAnyObjectPlaced = null;
    }

    //gets counter top point for attached child counter
    public Transform GetKitchenObjectFollowTransform()
    {
        
        return counterTopPoint;
    }

    //basically moves kitchen object to counter given (moves the object and animation and prefab)
    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager)
    {
        this.kitchenObjectManager = kitchenObjectManager;

        if (kitchenObjectManager != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    //used in all child counters - gets the kitchenobj manager so they can check if theres plate, spawn ingredient, destory kitchen obj, etc
    public KitchenObjectManager GetKitchenObjectManager()
    {
        return kitchenObjectManager;
    }

    //basically clears kitchenobj manager from whatever kitchen objects it has
    public void ClearKitchenObject()
    {
        kitchenObjectManager = null;
    }

    //does as it says - check if kitchen obj manager has kitchen objects stored
    public bool HasKitchenObject()
    {
        return kitchenObjectManager != null;
    }
}

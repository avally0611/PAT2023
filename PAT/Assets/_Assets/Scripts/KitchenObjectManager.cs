using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectManager : MonoBehaviour
{
    [SerializeField] private KitchenObjects kitchenobject;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjects GetKitchenObjects() 
    { 
        return kitchenobject; 
    }

    //basically responsible for moving the visual of object between player and counters
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //clear previous/parent object
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            //checking if new counter has object already
            Debug.LogError("Kitchen object parent already has a kitchen object");
        }

        //setting kitchen object to new counter
        kitchenObjectParent.SetKitchenObject(this);
        
        //update visual
        //basically setting the parent counter position to the given counter's object placing point (second counter) - moves object to other counter ---- changes parent
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class KitchenObjectManager : MonoBehaviour
{
    [SerializeField] private KitchenObjects kitchenobject;

    private IKitchenObjectParent kitchenObjectParent;

    //basically gets the Kitchen Object Scriptable objects
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
    
    //makes kitchen object disappear - e.g throw smth in the trash
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    //basically checks if plate exists - can be used to check if player holfing plate or plate on counter...
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else 
        { 
            plateKitchenObject= null;
            return false; 
        }
    }
    
    //does as it says - spawns a kitchenobject 
    public static KitchenObjectManager SpawnKitchenObject(KitchenObjects kitchenObjects, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjTransform = Instantiate(kitchenObjects.prefab);

        //placing kitchen object in player hands
        KitchenObjectManager kitchenObjectManager = kitchenObjTransform.GetComponent<KitchenObjectManager>();

        kitchenObjectManager.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObjectManager;
    }
}

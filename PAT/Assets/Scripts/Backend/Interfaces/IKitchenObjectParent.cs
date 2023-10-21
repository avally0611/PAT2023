using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager);


    public KitchenObjectManager GetKitchenObjectManager();


    public void ClearKitchenObject();


    public bool HasKitchenObject();

 


}

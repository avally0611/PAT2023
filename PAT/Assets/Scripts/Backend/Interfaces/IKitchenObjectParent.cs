using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface methods that most counters need - explained method in base counter
public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager);


    public KitchenObjectManager GetKitchenObjectManager();


    public void ClearKitchenObject();


    public bool HasKitchenObject();

 


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    
    [SerializeField] private GameObject Selected;
    [SerializeField] private KitchenObjects kitchenObjects;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private Player player;

    private KitchenObjectManager kitchenObjectManager;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.Escape))    
        {
            if (kitchenObjectManager != null)
            {
                kitchenObjectManager.SetKitchenObjectParent(secondClearCounter);
                
            }
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        
    }

   

    public void Interact(Transform interactableObjectTransform)
    {
        
        DisableOtherCounters();
        Selected.SetActive(true);

        if (kitchenObjectManager == null)
        {
            //clones specified object and retunrs at spec point
            Transform kitchenObjTransform = Instantiate(kitchenObjects.prefab, counterTopPoint);

            //placing kitchen object on new counter
            kitchenObjTransform.GetComponent<KitchenObjectManager>().SetKitchenObjectParent(this);

            //move object relative to parent
            kitchenObjTransform.localPosition = Vector3.zero;


        }
        else
        {
           
            //give object to player
            kitchenObjectManager.SetKitchenObjectParent(player);
            
        }
        


    }


    public Transform GetTransform()
    {
        return transform;
    }

    public void DisableOtherCounters()
    {
        //makes an array of all counter objects
        ClearCounter[] counters = FindObjectsOfType<ClearCounter>();

        for (int i = 0; i < counters.Length; i++)
        {
            if (counters[i] != this)
            {
                counters[i].Selected.SetActive(false);
            }

        }

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        //gets counter top point for attached counter
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager)
    {
        this.kitchenObjectManager = kitchenObjectManager;
    }

    public KitchenObjectManager GetKitchenObject()
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
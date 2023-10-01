using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    
    [SerializeField] private GameObject Selected;
    [SerializeField] private KitchenObjects kitchenObjects;
    [SerializeField] private Transform counterTopPoint;

    public void Interact(Transform interactableObjectTransform)
    {
        DisableOtherCounters();
        Selected.SetActive(true);

        //clones specified object and retunrs at spec point
        //placing the tomato at  local origin point - have  same position as  parent GameObject (in this case, counterTopPoint) because its local position relative to the parent is now zero
        Transform kitchenObjTransform = Instantiate(kitchenObjects.prefab, counterTopPoint);
        kitchenObjTransform.localPosition = Vector3.zero;


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

}
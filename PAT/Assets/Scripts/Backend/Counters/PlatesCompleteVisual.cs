using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCompleteVisual : MonoBehaviour
{
    //similar to classes except theyre not references - stored directly - used for storing small amounts of data (contains data not references) JUST STORE DATA
    [Serializable]

    //basically like making your object time - or your own little Person class
    public struct KitchenObjects_GameObject
    {
        public GameObject gameObject;
        public KitchenObjects kitchenObjects;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private KitchenObjects_GameObject[] kitchenObjectsGameObjArr;

    private void Start()
    {

        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        for (int i = 0; i < kitchenObjectsGameObjArr.Length; i++)
        {
           //make sure at start of game theres no visual of completed recipe
            kitchenObjectsGameObjArr[i].gameObject.SetActive(false);
            
        }
    }

    
    //when evet happens - when object put pn plate - add each obj to array and then show the visual of completed recipe on plate
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        for (int i = 0; i < kitchenObjectsGameObjArr.Length; i++)
        {
            if (kitchenObjectsGameObjArr[i].kitchenObjects == e.kitchenObjects)
            {
                kitchenObjectsGameObjArr[i].gameObject.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        //if i were too use array it would be much more complex, this is easier
        //loops through child objects of parent - basically loops through all the icons of the kitchen objects
        foreach (Transform child in transform)
        {
            //continue = if the current child = iconTemplate, it skips the current child and goes to next child/loop
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        KitchenObjects[] kitchenObjectsArr = plateKitchenObject.GetKitchenObjectsArr();

        
        for (int i = 0; i < plateKitchenObject.GetArrCount(); i++)
        {

            //why transform? - object is spawned as child of object
            //spawns icon not image
            Transform iconTransform = Instantiate(iconTemplate, transform);
      
            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjects(kitchenObjectsArr[i]);
            




        }
    }
}

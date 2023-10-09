using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    //represents a sinlge icon whihc holds image

    [SerializeField] private Image image;


    public void SetKitchenObjects(KitchenObjects kitchenObjects)
    {
        //basically getting the icon for the kitchen object and assigning it to the icon in the little ui thingie
        image.sprite = kitchenObjects.sprite;
    }
}

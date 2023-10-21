using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    //recipe has multiple items
    public KitchenObjects[] kitchenObjectsArr;
    public string recipeName;
}

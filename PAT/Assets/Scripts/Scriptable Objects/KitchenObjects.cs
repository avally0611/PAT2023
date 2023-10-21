using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//its basically like inheritance & its like an Object class  - you can make diff objects from single class
[CreateAssetMenu()]
public class KitchenObjects : ScriptableObject
{
    public Transform prefab;
    //for the food image
    public Sprite sprite;
    public string objectName;
}

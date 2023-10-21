using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjects input;
    public KitchenObjects output;
    public float burningTimerMax;
}

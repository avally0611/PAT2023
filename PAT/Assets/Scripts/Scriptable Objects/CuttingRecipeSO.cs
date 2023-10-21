using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjects input;
    public KitchenObjects output;
    public int cuttingProgressMax;
}

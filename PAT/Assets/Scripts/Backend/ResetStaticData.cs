using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//restes all the static events as they save thei rprevious data - which you dont want - NEW Game = previosu data
public class ResetStaticData : MonoBehaviour
{
    //when you move between scenes, all objects are destroyed except static ones which could cause errors so we gotta reset them

    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}

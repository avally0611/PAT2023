using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;

    private GameObject[] plateVisualGameObjectArr;
    private int count;

    private void Awake()
    {
        //make plates that can be on counter is 4
        plateVisualGameObjectArr = new GameObject[4];
    }

    private void Start()
    {
        
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        //decrease plate from array
        int indexOfRemovedObj = count - 1;

        //updates visual on counter
        GameObject plateGameObject = plateVisualGameObjectArr[indexOfRemovedObj];
        Remove(indexOfRemovedObj);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        //spawns plate & visual
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;

        //put plate on top of counter
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * count  ,0);

        plateVisualGameObjectArr[count] = plateVisualTransform.gameObject;
        count++;


    }

    //remove plate from array
    private void Remove(int index)
    {
        for (int i = index + 1; i < plateVisualGameObjectArr.Length; i++)
        {
            plateVisualGameObjectArr[i - 1] = plateVisualGameObjectArr[i];
        }

        count--;
    }
}

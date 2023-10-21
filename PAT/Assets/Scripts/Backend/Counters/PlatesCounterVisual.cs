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
        plateVisualGameObjectArr = new GameObject[4];
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        int indexOfRemovedObj = count - 1;
        GameObject plateGameObject = plateVisualGameObjectArr[indexOfRemovedObj];
        Remove(indexOfRemovedObj);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;

        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * count  ,0);

        plateVisualGameObjectArr[count] = plateVisualTransform.gameObject;
        count++;


    }

    private void Remove(int index)
    {
        for (int i = index + 1; i < plateVisualGameObjectArr.Length; i++)
        {
            plateVisualGameObjectArr[i - 1] = plateVisualGameObjectArr[i];
        }

        count--;
    }
}

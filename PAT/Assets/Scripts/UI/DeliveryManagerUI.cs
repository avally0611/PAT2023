using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] DeliveryManager manager;

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        manager.OnRecipeSpawned += Manager_OnRecipeSpawned;
        manager.OnRecipeCompleted += Manager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void Manager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void Manager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
           
            Destroy(child.gameObject);
           
        }

        RecipeSO[] recipeSOArr = manager.GetWaitingRecipeSoArr();


        for (int i = 0; i < manager.GetArrCount(); i++)
        {
            
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSOArr[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactableObjectTransform)
    {
        Debug.Log("Im a counter");
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
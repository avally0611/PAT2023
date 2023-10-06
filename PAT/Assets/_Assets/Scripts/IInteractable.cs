using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    //so when you reference interface from any class, the class can use these methods

    void Interact(Player player);

    void KitchenAction(Player player);

    Transform GetTransform();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    //so when you reference interface from any class, the class can use these methods

    void InteractPrimary(Player player);

    void InteractSecondary(Player player);

    Transform GetTransform();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface for all the interactiosn - allows player class to call method once & and it calls the particular object (closest counter)
public interface IInteractable
{
    //so when you reference interface from any class, the class can use these methods

    void InteractPrimary(Player player);

    void InteractSecondary(Player player);

    Transform GetTransform();
}

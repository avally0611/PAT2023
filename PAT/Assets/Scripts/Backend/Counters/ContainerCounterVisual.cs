using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    //made a string for animation name to avoid syntax errors
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //event that is invoked in container class - listened to here
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    //basically when player presses E by container - do the opening counter animation
    //this is an event method - listener method (subscriber method)
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

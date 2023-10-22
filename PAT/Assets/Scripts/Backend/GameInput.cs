using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles all input with unity cool input system
public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractPrimary;
    public event EventHandler OnInteractSecondary;
    public event EventHandler OnPauseAction;

    //part of unity input system
    private PlayerInputActions playerInputActions;

    private void Awake()
    {

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //interact_performed listens for when E is pressed
        playerInputActions.Player.InteractPrimary.performed += InteractPrimary_performed;

        //interact_performed listens for when F is pressed
        playerInputActions.Player.InteractSecondary.performed += InteractSecondary_performed;

        //interact_performed listens for when Esc is pressed
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    //good technique when changing between scenes = ie cleanup task
    private void OnDestroy()
    {
        //unsubscribing from events

        playerInputActions.Player.InteractPrimary.performed -= InteractPrimary_performed;
        playerInputActions.Player.InteractSecondary.performed -= InteractSecondary_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }
    
    //so when this button pressed all listeners are informed
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    //so when this button pressed all listeners are informed
    private void InteractSecondary_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractSecondary?.Invoke(this, EventArgs.Empty);
    }

    //so when this button pressed all listeners are informed
    private void InteractPrimary_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPrimary?.Invoke(this, EventArgs.Empty);

    }

    public Vector2 GetMovementVectorNormalized ()
    {
        //it is like having 4 if statements and reading button pressed - read button pressed as 2D input
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); ;

       
        //if you press 2 keys at once, the player is going to move faster - therefore you need to normalize it
        inputVector = inputVector.normalized;

        return inputVector;

    }

}

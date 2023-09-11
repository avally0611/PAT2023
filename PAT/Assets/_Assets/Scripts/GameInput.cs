using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //interact_performed listens for when E is pressed
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
        
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

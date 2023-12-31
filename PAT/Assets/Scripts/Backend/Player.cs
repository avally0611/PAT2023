using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles player movement and interaction
public class Player : MonoBehaviour, IKitchenObjectParent

{
    //show in the unity editor (serialized)

    [SerializeField] private float moveSpeed = 7f;
    //where object placed on player
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] GameInput gameInput;
    [SerializeField] GameManager gameManager;

    //for animation
    private bool isWalking;
    private Vector3 lastInteraction;
    private KitchenObjectManager kitchenObjectManager;


    public event EventHandler OnPickedSomething;


    //runs every frame
    private void Update()
    {
        HandleMovement();

    }

    private void Start()
    {
        //listening for keypress 
        gameInput.OnInteractPrimary += GameInput_OnInteractPrimary;
        gameInput.OnInteractSecondary += GameInput_OnInteractSecondary;
        
    }

    //on 'F' pressed = get closest counter and call that action in that class
    private void GameInput_OnInteractSecondary(object sender, System.EventArgs e)
    {
        //dont need {} when doing early return
        if (!gameManager.IsGamePlaying())
            return;

        IInteractable interactable = GetInteractableObject();


        if (interactable != null)
        {

            interactable.InteractSecondary(this);

        }
    }

    //if 'E' pressed - gets object interacted with and then calls that specific object interact method
    private void GameInput_OnInteractPrimary(object sender, System.EventArgs e)
    {
        if (!gameManager.IsGamePlaying())
            return;

        IInteractable interactable = GetInteractableObject();


        if (interactable != null)
        {

            interactable.InteractPrimary(this);

        }
    
        

    }


    public bool IsWalking()
    {
        return isWalking;
    }

    //handles all the movement - WASD keys 
    private void HandleMovement()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //transform applies to script it is in which refers to object it is attached to 
        //transform is a vector 3


        //don't want users to fly so make y 0 - put x as Vector2 x and z as Vector2 
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        //cast checks if player colliding with object - returns boolean
        // caast is a physics operation that fires a laser from a certain point ot check if it hits something - this boolean is true when object does not hit something
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        //Time.deltaTime makes sure that the player moves at same moveSpeed on any comp - BECAUSE comps have diff fps which makes the player move faster & it is how many secs it take to render a frame

        if (!canMove)
        {
            //cannot move towards move direction 

            //attempt moving only on x axis (forward and back)
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                //can only move on x
                moveDirection = moveDirectionX;
            }
            else
            {
                //try move on z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    //can only move on z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //cant move at all
                }

            }


        }

        if (canMove)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }


        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        //check is player moving 
        isWalking = moveDirection != Vector3.zero;

    }



    //gets the closest object interacted with and parses to GameInput_OnInteractPrimary
    public IInteractable GetInteractableObject()
    {
        IInteractable[] interactableObjects = new IInteractable[500];

        float interactRange = 1f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);


        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out IInteractable interactable))
            {
                interactableObjects[i] = interactable;  
            }
        }

        //to prioritize closest object
        IInteractable closestInteractable = null;

        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactableObjects[i];
            }
            else
            {
                //does a calc to see if previous or current object is closer to player 
                //also note how it uses GetTransform from interface class pertaining to specific object
                if (interactableObjects[i] != null && Vector3.Distance(transform.position, interactableObjects[i].GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactableObjects[i];

                }

            }

        }
        
        return closestInteractable;
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        //gets counter top point for attached counter
        return kitchenObjectHoldPoint;
    }

    //shows player picked up object
    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager)
    {
        this.kitchenObjectManager = kitchenObjectManager;

        if (kitchenObjectManager != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    //gets the kitchenobj msnager on player
    public KitchenObjectManager GetKitchenObjectManager()
    {
        return kitchenObjectManager;
    }

    //clears object from player hands
    public void ClearKitchenObject()
    {
        kitchenObjectManager = null;
    }

    //checks if player has object
    public bool HasKitchenObject()
    {
        return kitchenObjectManager != null;
    }
}

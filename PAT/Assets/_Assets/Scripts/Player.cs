using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    //show in the unity editor (serialized)
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking;

    private Vector3 lastInteraction;

    [SerializeField] GameInput gameInput;   

    //runs every frame
    private void Update()
    {
        HandleMovement();

        HandleInteractions();

        
        

    }

    public bool IsWalking()
    {
        return isWalking;
    }

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
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                //can only move on x
                moveDirection = moveDirectionX;
            }
            else
            {
                //try move on z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

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

    private void HandleInteractions()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float interactDistance = 2f;

        if (moveDirection != Vector3.zero)
        {
            //this stores the last position before stopped moving
            lastInteraction = moveDirection;
        }

        //out means the function is going to ouput/print smth
        //Raycast - shoot a laser from this position to this pos to check if character is interacting with smth - 
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance))
        {
            //return position of object hit
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //if the raycasthit detects counter - if player collided into counter
                clearCounter.Interact();
            }
        }
        


    }
}

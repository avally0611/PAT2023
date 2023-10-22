using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this whole class essentially orientate things - so loading bars will face the camera not the player , when player picks/places it looks at camera
public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;


    //after update essesntially - after object does its thing in update
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                //telling object to look at camera
                transform.LookAt(Camera.main.transform);
                break;

            case Mode.LookAtInverted:
                //gets distance from object to camera
                Vector3 dirFromCamera = transform.position - Camera.main.transform.forward;
                //make object look at exactly opposite point
                transform.LookAt(transform.position + dirFromCamera);
                break;

            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;

            case Mode.CameraForwardInverted:
                transform.forward = - Camera.main.transform.forward;
                break;
        }

        
        
    }

    


}

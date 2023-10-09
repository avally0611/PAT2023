using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    // this waits for at least one frame to render so the loading screen appears

    //ensures that the loading screen appears for at least one frame before transitioning to the target scene.
    private bool isFirstUpdate = true;

    private void Update()
    {
        //checks if it is the first frame and makes sure loading screen stays on
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallBack();
        }

        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface for classes or objects that use loading bars
public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    //holds detail about event - when event is fired it accesses the variable
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalised;
    }

}

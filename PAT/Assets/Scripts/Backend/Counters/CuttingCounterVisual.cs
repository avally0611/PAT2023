using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    
    //string for animation name
    private const string CUT = "Cut";
    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //when player starts cutting cue evemt
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        //cue cutting animation
        animator.SetTrigger(CUT);
    }

  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    //animation name
    private const string IS_WALKING = "isWalking";

    [SerializeField] private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}

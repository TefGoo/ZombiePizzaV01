using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_CROUCHING = "IsCrouching";
    private const string IS_ATTACKING = "IsAttacking";
    [SerializeField] private Player player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_RUNNING, player.IsRunning());
        animator.SetBool(IS_CROUCHING, player.IsCrouching());
        animator.SetBool(IS_ATTACKING, player.IsAttacking());
    }
}
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_ATTACKING = "IsAttacking";
    private const string IS_CROUCHING = "IsCrouching";

    private Player player;
    private Animator[] animators;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animators = GetComponentsInChildren<Animator>();
    }

    private void Update()
    {
        bool isWalking = player.IsWalking();
        bool isRunning = player.IsRunning();
        bool isAttacking = player.IsAttacking();
        bool isCrouching = player.IsCrouching();

        foreach (Animator animator in animators)
        {
            animator.SetBool(IS_WALKING, isWalking);
            animator.SetBool(IS_RUNNING, isRunning);
            animator.SetBool(IS_ATTACKING, isAttacking);
            animator.SetBool(IS_CROUCHING, isCrouching);
        }
    }
}
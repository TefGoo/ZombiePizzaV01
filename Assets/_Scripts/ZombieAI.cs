using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float idleTime = 2f;
    public float followDistance = 5f;
    public float attackDistance = 1.5f;
    public float followSpeed = 2f;
    public float attackDelay = 0.3f; // Delay before dealing damage

    private enum ZombieState
    {
        Idle,
        FollowPlayer,
        Attack
    }

    private ZombieState currentState = ZombieState.Idle;
    private float currentIdleTime;
    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentIdleTime = idleTime;
    }

    private void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                IdleState();
                break;
            case ZombieState.FollowPlayer:
                FollowPlayerState();
                break;
            case ZombieState.Attack:
                AttackState();
                break;
        }
    }

    private void IdleState()
    {
        currentIdleTime -= Time.deltaTime;

        if (currentIdleTime <= 0f)
        {
            currentState = ZombieState.FollowPlayer;
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsFollowingPlayer", true);
        }
    }

    private void FollowPlayerState()
    {
        if (player == null)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = idleTime;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = ZombieState.Attack;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsAttacking", true);
        }
        else if (distanceToPlayer > followDistance)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = idleTime;
        }
        else
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
        }
    }

    private void AttackState()
    {
        if (player == null)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = idleTime;
            return;
        }

        if (!isAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // Check if attack animation is playing
        {
            // Call the DelayedDamagePlayer coroutine to deal damage after the delay
            StartCoroutine(DelayedDamagePlayer());
            isAttacking = true;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            currentState = ZombieState.FollowPlayer;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsFollowingPlayer", true);
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // Check if attack animation is finished
        {
            isAttacking = false;  // Reset the isAttacking flag
        }
    }

    private IEnumerator DelayedDamagePlayer()
    {
        yield return new WaitForSeconds(attackDelay);

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            currentState = ZombieState.Idle;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = idleTime;
        }
    }
}
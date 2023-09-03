using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float attackDistance = 1.5f;
    public float attackDelay = 0.3f;
    public float idleTime = 2f;

    private enum EnemyState
    {
        Idle,
        Attack
    }

    private EnemyState currentState = EnemyState.Idle;
    private float currentIdleTime;
    private bool isAttacking;

    private NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentIdleTime = Random.Range(1f, 3f);
        agent = GetComponentInParent<NavMeshAgent>(); // Find NavMeshAgent in parent object
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Attack:
                AttackState();
                break;
        }
    }

    private void IdleState()
    {
        currentIdleTime -= Time.deltaTime;

        if (currentIdleTime <= 0f)
        {
            currentState = EnemyState.Attack;
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttacking", true); // Make sure this parameter is set
        }
    }

    private void AttackState()
    {
        if (player == null)
        {
            currentState = EnemyState.Idle;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
            agent.ResetPath();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            if (!isAttacking)
            {
                // Trigger the attack animation here if not already attacking
                animator.SetBool("IsAttacking", true);
                StartCoroutine(DelayedDamagePlayer());
                isAttacking = true;
            }
        }
        else
        {
            currentState = EnemyState.Idle;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
            agent.ResetPath();
        }
    }

    private System.Collections.IEnumerator DelayedDamagePlayer()
    {
        yield return new WaitForSeconds(attackDelay);

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Invoke("ScreenShake", 0f);
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void ScreenShake()
    {
        // Implement your screen shake logic here
    }
}

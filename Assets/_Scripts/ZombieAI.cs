using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform destination; // Destination for tower defense behavior
    public Transform destination1; // First destination for tower defense behavior
    public Transform destination2; // Second destination for tower defense behavior
    public float towerDefenseDistance = 10f; // Distance at which the zombie switches to tower defense behavior
    public float attackDistance = 1.5f;
    public float followSpeed = 2f;
    public float attackDelay = 0.3f; // Delay before dealing damage

    public float towerDefenseSpeed = 2f; // Speed when in tower defense mode
    public float followPlayerSpeed = 4f; // Speed when following the player

    private enum ZombieState
    {
        Idle,
        TowerDefense,
        FollowPlayer,
        Attack
    }

    private ZombieState currentState = ZombieState.Idle;
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
            case ZombieState.Idle:
                IdleState();
                break;
            case ZombieState.TowerDefense:
                TowerDefenseState();
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
            currentState = ZombieState.TowerDefense;
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsTowerDefense", true);
            ChooseClosestDestination();
        }
    }

    private void ChooseClosestDestination()
    {
        // Check which destination is closer and set it as the new destination
        float distanceToDestination1 = Vector3.Distance(transform.position, destination1.position);
        float distanceToDestination2 = Vector3.Distance(transform.position, destination2.position);

        if (distanceToDestination1 < distanceToDestination2)
        {
            agent.SetDestination(destination1.position);
        }
        else
        {
            agent.SetDestination(destination2.position);
        }
    }

    private void TowerDefenseState()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= towerDefenseDistance)
        {
            SwitchToAI();
        }
        else if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsTowerDefense", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
        }
        else
        {
            // Set the speed to the towerDefenseSpeed when in tower defense mode
            agent.speed = towerDefenseSpeed;
            currentState = ZombieState.TowerDefense;
            animator.SetBool("IsTowerDefense", true);
            animator.SetBool("IsIdle", false);
        }
    }

    private void FollowPlayerState()
    {
        if (player == null)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
            agent.ResetPath();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = ZombieState.Attack;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsAttacking", true);
        }
        else if (distanceToPlayer > towerDefenseDistance)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsFollowingPlayer", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
            agent.ResetPath();
        }
        else
        {
            // Set the speed to the followPlayerSpeed when following the player
            agent.speed = followPlayerSpeed;
            agent.SetDestination(player.position); // Set destination to player position
        }
    }

    private void AttackState()
    {
        if (player == null)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
            currentIdleTime = Random.Range(1f, 3f);
            agent.ResetPath();
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

    private System.Collections.IEnumerator DelayedDamagePlayer()
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

    private void SwitchToAI()
    {
        currentState = ZombieState.FollowPlayer;
        animator.SetBool("IsTowerDefense", false);
        animator.SetBool("IsFollowingPlayer", true);
        agent.ResetPath();
    }
}
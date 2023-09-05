using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform[] destinations; // Array of destinations for tower defense behavior
    public float towerDefenseDistance = 10f; // Distance at which the zombie switches to tower defense behavior
    public float attackDistance = 1.5f;
    public float followSpeed = 2f;
    public float attackDelay = 0.3f; // Delay before dealing damage

    public float towerDefenseSpeed = 2f; // Speed when in tower defense mode
    public float followPlayerSpeed = 4f; // Speed when following the player
    [SerializeField] CinemachineImpulseSource _impulseSource;

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
    private int currentDestinationIndex = 0; // Index of the current destination

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentIdleTime = Random.Range(1f, 2f);
        agent = GetComponentInParent<NavMeshAgent>(); // Find NavMeshAgent in parent object
        // Do not choose the initial destination here.
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
        }
    }

    private void ChooseDestination()
    {
        // Use the current destination index
        agent.SetDestination(destinations[currentDestinationIndex].position);
    }
    private bool hasLoggedDestination = false; // Add this variable

    private void TowerDefenseState()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= towerDefenseDistance)
        {
            SwitchToAI();
        }
        else if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            // Choose a random destination from the array
            int randomDestinationIndex = Random.Range(0, destinations.Length);
            agent.SetDestination(destinations[randomDestinationIndex].position);

            // Log the selected destination index only if it hasn't been logged before
            if (!hasLoggedDestination)
            {
                Debug.Log("Selected destination index: " + randomDestinationIndex);
                hasLoggedDestination = true; // Set the flag to true to prevent further logging
            }
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
                Invoke("ScreenShake", 0f);
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void ScreenShake()
    {
        _impulseSource.GenerateImpulse();
    }

    private void SwitchToAI()
    {
        currentState = ZombieState.FollowPlayer;
        animator.SetBool("IsTowerDefense", false);
        animator.SetBool("IsFollowingPlayer", true);
        agent.ResetPath();
    }
}
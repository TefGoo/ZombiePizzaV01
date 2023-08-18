using UnityEngine;
using UnityEngine.AI;

public class NewZombieAi : MonoBehaviour
{
    public Animator animator;
    public Transform[] randomPoints; // Random points the zombie will move between
    public float moveSpeed = 1f; // Zombie's movement speed

    private Transform parentTransform; // Reference to the parent's transform
    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>(); // Get the NavMeshAgent from the parent object
        parentTransform = transform.parent; // Store the parent's transform
        MoveToRandomPoint();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToRandomPoint();
        }
    }

    private void MoveToRandomPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % randomPoints.Length;
        Vector3 destination = parentTransform.TransformPoint(randomPoints[currentPointIndex].position);
        agent.SetDestination(destination);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    private NavMeshAgent agent;
    private WayPoint[] waypoints;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float standStillTimeLength = 1;
    [SerializeField] private float targetDistanceToWaypoint = 5;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float runSpeed = 10;
    private float standStillTimer = 0;
    private bool hasArrived = false;
    private static readonly int isWalking = Animator.StringToHash("IsWalking");
    private bool isRunning = false;

    //will give us a random waypoint in the array as a variable every time i access it
    private WayPoint RandomPoint => waypoints[Random.Range(0, waypoints.Length)];

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        // FindObjectsOfType gets every instanc of this component in the scene.
        waypoints = FindObjectsOfType<WayPoint>();

        // Tell the agent to move ot a random position in the scen waypoints
        agent.stoppingDistance = targetDistanceToWaypoint;
    }

    private void UpdateMovement()
    {
        if (hasArrived || (!agent.pathPending && agent.remainingDistance < targetDistanceToWaypoint))// has the agent reached it's position
        {
            if(standStillTimer > standStillTimeLength)
            {
                isRunning = false;
                animator.SetBool(isWalking, true);
                if(Random.Range(0, 2) == 1)
                {
                    animator.SetTrigger("Run");
                    isRunning = true;
                }
                agent.speed = isRunning
                    ? runSpeed
                    : walkSpeed;
                standStillTimer = 0;
                // Tell the agent to move ot a random position in the scen waypoints
                agent.SetDestination(RandomPoint.Position);
                hasArrived = false;
                return;
            }
            hasArrived = true;
            animator.SetBool(isWalking, false);
            standStillTimer += Time.deltaTime;
        }
        else
        {
            animator.SetBool(isWalking, true);
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
}

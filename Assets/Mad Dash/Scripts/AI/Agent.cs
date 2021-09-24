using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    private NavMeshAgent agent;
    private WayPoint[] waypoints;
    
    [Header("-- Animation Settings --")]
    [SerializeField, Tooltip("This agent's animator")] private Animator animator;
    [Header("-- Movement Settings --")]
    [SerializeField, Tooltip("how long to stand still after reaching the destination")] private float standStillTimeLength = 1;
    [SerializeField, Tooltip("How far away from the destination will the AI stop")] private float targetDistanceToWaypoint = 5;
    [SerializeField, Tooltip("how fast the agent will walk")] private float walkSpeed = 5;
    [SerializeField, Tooltip("how fast the agent will run")] private float runSpeed = 10;
    //how long the agent will stand still for
    private float standStillTimer = 0;
    //whether the agent has arrived yet
    private bool hasArrived = false;
    //the animation thing. rider tells me this is more efficient.
    private static readonly int isWalking = Animator.StringToHash("IsWalking");
    //whether or not the agent is running
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

    /// <summary>
    /// This will update which target the ai should be navigating towards and also the animation based on what the ai is doing
    /// </summary>
    private void UpdateMovement()
    {
        // has the agent reached its position
        if (hasArrived || (!agent.pathPending && agent.remainingDistance < targetDistanceToWaypoint))// has the agent reached it's position
        {
            //if the agent has stood still for standstilltimelength
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
            //set the animation to the idle animation and increase the timer until it is greater than standstilltimelength
            hasArrived = true;
            animator.SetBool(isWalking, false);
            standStillTimer += Time.deltaTime;
        }
        else
        {
            //make sure the agent is using a moving animation
            animator.SetBool(isWalking, true);
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmartAgent : MonoBehaviour
{
    //this ai agent
    private NavMeshAgent agent;
    
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

    //the list of all smart ai targets, these will be sorted in order of priority, so that the agent will prioritise the targets with the lower priority
    private List<SmartAiTarget> aiTargets = new List<SmartAiTarget>();
    //the ai target that the ai is going towards
    private SmartAiTarget currentAiTarget;

    //whether or not the ai has gone through all the targets
    private bool targetsExhausted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //get the navmeshAgent component attatched to this gameobject
        agent = gameObject.GetComponent<NavMeshAgent>();

        //add all smartaitargets in the scene to aiTargets
        foreach(SmartAiTarget smartAiTarget in GameObject.FindObjectsOfType<SmartAiTarget>())
        {
            aiTargets.Add(smartAiTarget);
        }
        //use link to order the aiTargets by their priority, so that the agent will go towards them first.
        aiTargets = aiTargets.OrderBy(x => x.AiPriority).ToList();
        
        // Tell the agent to move ot a random position in the scen waypoints
        agent.stoppingDistance = targetDistanceToWaypoint;
    }

    /// <summary>
    /// returns the next SmartAiTarget with the lowest aiPriority which has not been activated and can be navigated to.
    /// </summary>
    /// <returns>The next SmartAiTarget with the lowest aiPriority which has not been activated and can be navigated to.</returns>
    private SmartAiTarget NextDestination()
    {
        //iterating over aiTargets in order will ensure that the SmartAiTarget that is returned will have the lowest priority, since it has been sorted.
        for(int i = 0; i < aiTargets.Count; i++)
        {
            //if the ai target has not been interracted with and it can be navigated to, return it.
            NavMeshPath path = new NavMeshPath();
            if(!aiTargets[i].Interracted && agent.CalculatePath(aiTargets[i].transform.position, path))
            {
                if(path.status == NavMeshPathStatus.PathComplete)
                {
                    return aiTargets[i];
                }
            }
        }
        return null;
    }
    
    /// <summary>
    /// This will update which target the ai should be navigating towards and also the animation based on what the ai is doing
    /// </summary>
    private void UpdateMovement()
    {
        if(!targetsExhausted)
        {
            // has the agent reached it's position
            if (hasArrived || (!agent.pathPending && agent.remainingDistance < targetDistanceToWaypoint))
            {
                //if the agent has stood still for standStillTimeLength
                if(standStillTimer > standStillTimeLength)
                {
                    // try to interract with the current ai target if it isn't null
                    if(currentAiTarget != null)
                    {
                        currentAiTarget.Interract();
                    }
                    // set the currentAiTarget to the next destination
                    currentAiTarget = NextDestination();
                    // break out of this function if the next destination is null
                    if(currentAiTarget == null)
                    {
                        return;      
                    }
                    // Tell the agent to move to the next prioritised smartAITarget
                    agent.SetDestination(currentAiTarget.transform.position);
                    
                    // randomly decide whether the agent should run or walk, and set the speed and animation accordingly
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
                    
                    //set hasArrived to false because the journey has only just begun
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
                //make sure the agent is using a moving animation.
                animator.SetBool(isWalking, true);
            }
        }
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMovement();
    }
}

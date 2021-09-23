using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmartAgent : MonoBehaviour
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

    private List<SmartAiTarget> aiTargets = new List<SmartAiTarget>();
    private SmartAiTarget currentAiTarget;

    private bool targetsExhausted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        foreach(SmartAiTarget smartAiTarget in GameObject.FindObjectsOfType<SmartAiTarget>())
        {
            aiTargets.Add(smartAiTarget);
        }
        aiTargets = aiTargets.OrderBy(x => x.AiPriority).ToList();
        
        // Tell the agent to move ot a random position in the scen waypoints
        agent.stoppingDistance = targetDistanceToWaypoint;
    }

    private SmartAiTarget NextDestination()
    {
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
    
    private void UpdateMovement()
    {
        if(!targetsExhausted)
        {
            if (hasArrived || (!agent.pathPending && agent.remainingDistance < targetDistanceToWaypoint))// has the agent reached it's position
            {
                if(standStillTimer > standStillTimeLength)
                {
                    // Tell the agent to move to the next prioritised smartAITarget
                    if(currentAiTarget != null)
                    {
                        currentAiTarget.Interract();
                    }
                    currentAiTarget = NextDestination();
                    if(currentAiTarget == null)
                    {
                        return;      
                    }
                    agent.SetDestination(currentAiTarget.transform.position);
                    
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
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
}

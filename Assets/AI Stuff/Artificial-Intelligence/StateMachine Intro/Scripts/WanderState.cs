using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : StateMachineBehaviour
{
    // like the start function of MonoBehavior except it runs only the first frame update is called in this state
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
    }

    // similar to monobehavior update method, runs once per update frame
    // EXCEPT the first and last fram that it is in the state for
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    // like the start function of monobehaviior except it runs only the last frame update is called in this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        //transition to new state
        animator.SetTrigger("BeginTarget");
    }
}

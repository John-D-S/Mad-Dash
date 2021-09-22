using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wander,
    Target,
    Attack,
    Damage,
    Die
}

public class CustomStateMachine : MonoBehaviour
{
    private Dictionary<State, string> stateCoroutines = new Dictionary<State, string>();

    private Coroutine currentCoroutine;

    private void Start()
    {
        //specify which states map to whic functions
        //stateCoroutines.Add(State.Wander, "WanderState");
        
        //this loops throught until we hit the die state
        for (int i = 0; i <= (int)State.Die; i++)
        {
            // convert the iterator to a state
            State state = (State)i;
            // construct the function's name as a string
            string functionName = state.ToString() + "State";

            // add the states to the dictionary
            stateCoroutines.Add(state, functionName);

            Debug.Log(state.ToString() + ", " + functionName);
        }

        SwapState(State.Wander);
    }
    public void SwapState(State _newState)
    {
        // is there a coroutine already running?
        if (currentCoroutine != null)
        {
            //there is, so stop it and make currentCoroutine null
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        //runs the coroutine, use the function version most of the time.
        //stores which coroutine we are currently running so we can stop it later
        currentCoroutine = StartCoroutine(stateCoroutines[_newState]);
    }

    private IEnumerator WanderState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//yeild is stopping and reading the return...
            Debug.Log("la de da da woo we woo");
        }
    }

    private IEnumerator TargetState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//yeild is stopping and reading the return...
            Debug.Log("I see you, bitch!");
        }
    }

    private IEnumerator AttackState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//yeild is stopping and reading the return...
            Debug.Log("Take this!");
        }
    }

    private IEnumerator DamageState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//yeild is stopping and reading the return...
            Debug.Log("OOF!");
        }
    }
    
    private IEnumerator DieState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//yeild is stopping and reading the return...
            Debug.Log("Why i die?");
        }
    }
}
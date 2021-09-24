using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartAiTarget : MonoBehaviour
{
    [SerializeField, Tooltip("How the smart ai agent will prioritise this target")] private int aiPriority;
    public int AiPriority => aiPriority;
    //gets whether or not the target has been interracted with
    public bool Interracted { get; private set; } = false;

    /// <summary>
    /// a special little function to execute specific functionality when the target has been interracted with, like changing a material
    /// </summary>
    protected abstract void InterractFunctionality();
    
    public void Interract()
    {
        //do the interract functionality and set interracted to true
        InterractFunctionality();
        Interracted = true;
    }
}

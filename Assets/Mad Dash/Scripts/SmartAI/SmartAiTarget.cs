using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartAiTarget : MonoBehaviour
{
    [SerializeField] private int aiPriority;
    public int AiPriority => aiPriority;
    public bool Interracted { get; private set; } = false;

    protected abstract void InterractFunctionality();
    
    public void Interract()
    {
        InterractFunctionality();
        Interracted = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCoin : SmartAiTarget
{
    [SerializeField] private MeshRenderer coinMesh;

    protected override void InterractFunctionality()
    {
        coinMesh.enabled = false;
    }
}

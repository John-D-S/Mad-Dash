using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCoin : SmartAiTarget
{
    [SerializeField, Tooltip("The MeshRender that will disappear when interracted with")] private MeshRenderer coinMesh;

    protected override void InterractFunctionality()
    {
        //make the coin disappear
        coinMesh.enabled = false;
    }
}

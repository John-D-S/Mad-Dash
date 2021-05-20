using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject doorPart;
    //the localPosition.y of the door at its lowest
    private float closedDoorHeight;
    //the localPosition.y of the door at its highest
    [SerializeField]
    private float openedDoorHeight = 10;
    [SerializeField]
    private float doorMoveSpeed = 3;
    private float DoorYpos => doorPart.transform.localPosition.y;

    private MeshRenderer GateMeshRenderer;

    [System.NonSerialized]
    public bool open = false;

    //gets the target height of the door based on whether or not the bool open is true
    float TargetHeight
    {
        get
        {
            if (open)
                return openedDoorHeight;
            else
                return closedDoorHeight;
        }
        set { }
    }

    private void SetDoorHeight(float _YPos) => doorPart.transform.localPosition = new Vector3(doorPart.transform.localPosition.x, _YPos, doorPart.transform.localPosition.z);

    public void SetColor(Color _color) => GateMeshRenderer.material.color = _color;

    private void Start()
    {
        closedDoorHeight = DoorYpos;
        GateMeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //if the door's height is below its target, move it up until it isn't; visa versa for if it is above it's target.
        if (DoorYpos < TargetHeight)
        {
            doorPart.transform.localPosition += Vector3.up * Time.deltaTime * doorMoveSpeed;
            if (DoorYpos > TargetHeight)
                SetDoorHeight(TargetHeight);
        }
        else if (DoorYpos > TargetHeight)
        {
            doorPart.transform.localPosition -= Vector3.up * Time.deltaTime * doorMoveSpeed;
            if (DoorYpos < TargetHeight)
                SetDoorHeight(TargetHeight);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoorFrame;
    [SerializeField]
    private GameObject rightDoorFrame;
    [SerializeField]
    private GameObject doorPart;

    //the localPosition.y of the door at its lowest
    private float closedDoorHeight;
    //the localPosition.y of the door at its highest
    [SerializeField]
    private float openedDoorHeight = 10;
    //the width of the door
    [SerializeField]
    private float doorWidth = 10;
    [SerializeField]
    private float doorMoveSpeed = 3;
    private float DoorYpos => doorPart.transform.localPosition.y;

    private MeshRenderer leftDoorFrameMeshRenderer;
    private MeshRenderer rightDoorFrameMeshRenderer;

    [System.NonSerialized]
    public bool open = false;

    //gets the target height of the door based on whether or not the bool open is true
    private float TargetHeight
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

    private void OnValidate()
    {
        //setting the position of the left and right doorframes as specified in the inspector
        Vector3 leftDoorFramePosition = Vector3.left * doorWidth * 0.5f + Vector3.up * openedDoorHeight * 0.5f;
        leftDoorFrame.transform.localPosition = new Vector3(-doorWidth * 0.5f, openedDoorHeight * 0.5f, 0);
        rightDoorFrame.transform.localPosition = new Vector3(doorWidth * 0.5f, openedDoorHeight * 0.5f, 0);
        
        //setting the scale of the doorframes to fit the height of the opened door;
        Vector3 doorFrameScale = new Vector3(1, openedDoorHeight, 1);
        leftDoorFrame.transform.localScale = doorFrameScale;
        rightDoorFrame.transform.localScale = doorFrameScale;

        doorPart.transform.localPosition = new Vector3(0, openedDoorHeight * 0.5f);
        doorPart.transform.localScale = new Vector3(1 * doorWidth - 1, openedDoorHeight, 1);
    }

    private void SetDoorHeight(float _YPos) => doorPart.transform.localPosition = new Vector3(doorPart.transform.localPosition.x, _YPos, doorPart.transform.localPosition.z);

    public void SetColor(Color _color) 
    { 
        leftDoorFrameMeshRenderer.material.color = _color;
        rightDoorFrameMeshRenderer.material.color = _color;
    }

    private void Start()
    {
        closedDoorHeight = DoorYpos;
        leftDoorFrameMeshRenderer = leftDoorFrame.GetComponent<MeshRenderer>();
        rightDoorFrameMeshRenderer = rightDoorFrame.GetComponent<MeshRenderer>();
        SetColor(Color.red);
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

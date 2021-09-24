using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("-- Door objects --")]
    [SerializeField, Tooltip("The left door frame GameObject, it should have a mesh renderer")]
    private GameObject leftDoorFrame;
    [SerializeField, Tooltip("The right door frame GameObject, it should have a mesh renderer")]
    private GameObject rightDoorFrame;
    [SerializeField, Tooltip("The part of the door that moves")]
    private GameObject doorPart;

    [Header("-- Door Movement settings --")]
    //the localPosition.y of the door at its lowest
    private float closedDoorHeight;
    [SerializeField, Tooltip("the localPosition.y of the door at its highest")]
    private float openedDoorHeight = 10;
    [SerializeField, Tooltip("the width of the door")]
    private float doorWidth = 10;
    [SerializeField, Tooltip("how fast the door opens and closes")]
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

    /// <summary>
    /// set the door height.
    /// </summary>
    private void SetDoorHeight(float _YPos) => doorPart.transform.localPosition = new Vector3(doorPart.transform.localPosition.x, _YPos, doorPart.transform.localPosition.z);

    /// <summary>
    /// set the color of the doorframe meshrenderers
    /// </summary>
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
            doorPart.transform.localPosition += Vector3.up * (Time.deltaTime * doorMoveSpeed);
            if (DoorYpos > TargetHeight)
                SetDoorHeight(TargetHeight);
                
        }
        else if (DoorYpos > TargetHeight)
        {
            doorPart.transform.localPosition -= Vector3.up * (Time.deltaTime * doorMoveSpeed);
            if (DoorYpos < TargetHeight)
                SetDoorHeight(TargetHeight);
        }
    }
}

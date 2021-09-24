using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Door))]
public class SmartDoor : MonoBehaviour
{
	private Door door;
	[SerializeField, Tooltip("All these buttons must be interracted with to open the door")] private List<SmartButton> requiredButtons = new List<SmartButton>();
	
	private void Start()
	{
		//initialise the door component.
		door = GetComponent<Door>();
	}

	private void FixedUpdate()
	{
		//if the door isn't open, check each of the Buttons to see if they've all been opened.
		//if all the buttons have been activated, open the door
		if(!door.open)
		{
			int noOfActivatedButtons = 0;
			for(int i = 0; i < requiredButtons.Count; i++)
			{
				if(requiredButtons[i].Interracted)
				{
					noOfActivatedButtons++;
				}
			}
			if(noOfActivatedButtons == requiredButtons.Count)
			{
				door.open = true;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		//draw red lines from the door to each of the required buttons
		Gizmos.color = Color.red;
		foreach(SmartButton requiredButton in requiredButtons)
		{
			if(requiredButton)
			{
				Gizmos.DrawLine(transform.position, requiredButton.transform.position);
			}
		}
	}
}

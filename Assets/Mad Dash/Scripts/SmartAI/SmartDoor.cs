using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Door))]
public class SmartDoor : MonoBehaviour
{
	private Door door;
	[SerializeField] private List<SmartButton> requiredButtons = new List<SmartButton>();
	
	private void Start()
	{
		door = GetComponent<Door>();
	}

	private void FixedUpdate()
	{
		if(!door.open)
		{
			int noOfActivatedLevers = 0;
			for(int i = 0; i < requiredButtons.Count; i++)
			{
				if(requiredButtons[i].Interracted)
				{
					noOfActivatedLevers++;
				}
			}
			if(noOfActivatedLevers == requiredButtons.Count)
			{
				door.open = true;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
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

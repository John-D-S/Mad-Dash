using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartButton : SmartAiTarget
{
	[SerializeField, Tooltip("The mesh renderer that will change color when interracted with")] private MeshRenderer colorMeshRenderer;

	private void Start()
	{
		//set the color to red to start with
		colorMeshRenderer.material.color = Color.red;
	}

	protected override void InterractFunctionality()
	{
		//change the color to green to indicate the button has been pressed
		colorMeshRenderer.material.color = Color.green;
	}
}

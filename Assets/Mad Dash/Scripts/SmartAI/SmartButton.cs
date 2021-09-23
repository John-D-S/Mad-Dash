using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartButton : SmartAiTarget
{
	[SerializeField] private MeshRenderer colorMeshRenderer;

	private void Start()
	{
		colorMeshRenderer.material.color = Color.red;
	}

	protected override void InterractFunctionality()
	{
		colorMeshRenderer.material.color = Color.green;
	}
}

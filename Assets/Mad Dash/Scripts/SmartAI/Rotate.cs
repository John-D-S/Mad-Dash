using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] private float rotationsPerSecond = 0.5f;

	private void FixedUpdate()
	{
		transform.Rotate(Vector3.up * (rotationsPerSecond * Time.fixedDeltaTime * 365));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
	private bool limited;

	public void ToggleLimitFramerate()
	{
		if(!limited)
		{
			Application.targetFrameRate = 20;
			limited = true;
		}

		if(limited)
		{
			Application.targetFrameRate = 300;
			limited = false;
		}
	}
}

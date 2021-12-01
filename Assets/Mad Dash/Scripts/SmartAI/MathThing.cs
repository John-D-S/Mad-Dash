using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This only exists to contain functions with param and returns things
/// </summary>
public class MathThing : MonoBehaviour
{
	/// <summary>
	/// adds two ints together
	/// </summary>
	/// <param name="a">the first int to add</param>
	/// <param name="b">the second int to add</param>
	/// <returns>the ints added together</returns>
	public int AddInts(int a, int b) => a + b;
	
	/// <summary>
	/// multiplies two ints
	/// </summary>
	/// <param name="a">the first int to multipliy</param>
	/// <param name="b">the second int to multiply</param>
	/// <returns>the ints multiplied</returns>
	public int MultiplyInts(int a, int b) => a * b;
}

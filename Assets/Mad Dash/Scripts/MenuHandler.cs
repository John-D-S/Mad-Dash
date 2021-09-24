using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
	[SerializeField, Tooltip("If true, pressing the cancel key (esc) will load the scene at build index 0")] private bool escLoadsSceneZero;
	[SerializeField, Tooltip("If true, pressing the r Key will reload this scene")] private bool rKeyReloadsScene;

	/// <summary>
	/// change to the scene with the given scene build index
	/// </summary>
	public void ChangeToScene(int _sceneIndex)
	{
		SceneManager.LoadScene(_sceneIndex);
	}
	
	/// <summary>
	/// quit the game
	/// </summary>
	public void QuitGame()
	{
		Application.Quit();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	private void Start()
	{
		//unlock the cursor if the current scene build index is 0
		if(SceneManager.GetActiveScene().buildIndex == 0)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	private void Update()
	{
		if(escLoadsSceneZero)
		{
			if(Input.GetAxisRaw("Cancel") > 0.5f)
			{
				ChangeToScene(0);
			}
		}
		if(rKeyReloadsScene)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				ChangeToScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}

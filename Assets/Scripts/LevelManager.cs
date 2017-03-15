using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class allows us to handle Levels (load,...)
/// </summary>
public class LevelManager : MonoBehaviour {
	public void LoadLevel(string _levelName)
	{
		SceneManager.LoadScene(_levelName);
	}

	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

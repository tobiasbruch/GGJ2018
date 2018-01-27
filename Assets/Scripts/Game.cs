using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	[SerializeField]
	private GameObject _gameOverScreen;

	[SerializeField]
	private Canvas _uiCanvas;

	// Use this for initialization
	void Start ()
	{
		var taskManager = Locator.Get<TaskManager>();
		taskManager.Init();
		taskManager.StartRandomTask();
	}

	public void GameOver()
	{
		Instantiate(_gameOverScreen, _uiCanvas.transform, false);
		Locator.Clear();
	}
}

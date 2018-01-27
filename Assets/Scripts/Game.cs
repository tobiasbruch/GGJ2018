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

	// Update is called once per frame
	void Update () {

	}

	public void ShowGameOver(){
		Instantiate(_gameOverScreen, _uiCanvas.transform, false);
	}
}

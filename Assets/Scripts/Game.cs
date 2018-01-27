using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleanStart : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Locator.Clear();
		SceneManager.LoadScene("Scenes/Game");
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

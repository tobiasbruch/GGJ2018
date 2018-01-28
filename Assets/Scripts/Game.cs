using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public float startTime;
	public float addedTimePerDelivering;

	float currentTime;

	[SerializeField]
	private GameObject _gameOverScreen;

	[SerializeField]
	private Canvas _uiCanvas;

	[SerializeField]
	private GameObject intro;

	float endTime;

	[SerializeField] SpriteRenderer backgroundImage;

	bool playing = true;

	void Start()
	{
		endTime = startTime/3;
		intro.gameObject.SetActive(true);
		currentTime = startTime;
	}
	public void GameOver()
	{
		Instantiate(_gameOverScreen, _uiCanvas.transform, false);
	}

	public void AddTime()
	{
		currentTime += addedTimePerDelivering;
	}

	float difficulty = 1;

	void Update()
	{
		difficulty += Time.deltaTime/100; // after 100 seconds, twice as hard
		if(!Locator.Get<Timer>().countdownDone) return;
		if(Locator.Get<TaskManager>().completedCount > 0)
		{
			currentTime -= (Time.deltaTime * difficulty);

			var p = startTime / currentTime;

			var pRed = (1 - (p-1)/2 );
			p = (1 - ((p-1)) );


			var color = new Color(pRed,p,p);

			backgroundImage.color = color;
		}

		float actualTimeLeft = currentTime - endTime;

		Locator.Get<Timer>().SetSeconds(actualTimeLeft);


		if(currentTime < endTime)
		{
			GameOver();
			playing = false;
		}
	}
}

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

	[SerializeField] SpriteRenderer backgroundImage;

	bool playing = true;

	void Start()
	{
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
		if(!playing) return;
		if(Locator.Get<TaskManager>().completedCount > 0)
		{
			currentTime -= (Time.deltaTime * difficulty);

			var p = startTime / currentTime;

			var pRed = (1 - (p-1)/2 );
			p = (1 - ((p-1)) );


			var color = new Color(pRed,p,p);

			backgroundImage.color = color;
		}

		if(currentTime < startTime/3)
		{
			GameOver();
			playing = false;
		}
	}
}

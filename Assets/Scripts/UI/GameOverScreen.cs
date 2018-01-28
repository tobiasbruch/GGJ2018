using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	[SerializeField] TextMeshProUGUI label;

	void Start()
	{
		label.text = Locator.Get<Resources>().coins.ToString();
		//label.transform.DOPunchScale(Vector3.one * 1.4f, .5f, 1, .1f);
		//label.transform.localScale = Vector3.one;
	}

	public void Retry()
	{
		SceneManager.LoadScene("Scenes/Empty");
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	[SerializeField] TextMeshProUGUI label;
	[SerializeField] TextMeshProUGUI countdown;


	public bool countdownDone = false;

	// Use this for initialization
	void Start () {
		label.gameObject.SetActive(false);
		countdown.text = "";
	}

	// Update is called once per frame
	void Update () {

	}


	public void Countdown()
	{
		StartCoroutine(DoCountdown());
	}

	IEnumerator DoCountdown()
	{
		yield return new WaitForSeconds(1);
		countdown.text = "3";
		countdown.transform.DOPunchScale(Vector3.one * 1.2f, 1f, 1, .1f);
		yield return new WaitForSeconds(1);
		countdown.text = "2";
		countdown.transform.DOPunchScale(Vector3.one * 1.2f, 1f, 1, .1f);
		yield return new WaitForSeconds(1);
		countdown.text = "1";
		countdown.transform.DOPunchScale(Vector3.one * 1.2f, 1f, 1, .1f);
		yield return new WaitForSeconds(1);
		countdown.text = "Go!";
		countdown.transform.DOPunchScale(Vector3.one * 1.2f, 1f, 1, .1f);
		label.gameObject.SetActive(true);
		label.transform.DOPunchScale(Vector3.one * 1.2f, .5f, 1, .1f);
		yield return new WaitForSeconds(1);
		countdown.text = "";
		countdownDone = true;
	}

	public void Bounce()
	{
		label.transform.DOKill();
		label.transform.DOPunchScale(Vector3.one * 1.2f, .5f, 1, .1f);
		label.transform.localScale = Vector3.one;
	}

	public void SetSeconds(float seconds)
	{
		var t = TimeSpan.FromSeconds( seconds );
		label.text = string.Format("{0:D2}:{1:D2}s",
			t.Minutes,
			t.Seconds);
	}
}

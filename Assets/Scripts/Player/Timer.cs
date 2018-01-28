using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	[SerializeField] TextMeshProUGUI label;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Bounce()
	{
		transform.DOKill();
		transform.DOPunchScale(Vector3.one * 1.2f, .5f, 1, .1f);
		transform.localScale = Vector3.one;
	}

	public void SetSeconds(float seconds)
	{
		var t = TimeSpan.FromSeconds( seconds );
		label.text = string.Format("{0:D2}:{1:D2}s",
			t.Minutes,
			t.Seconds);
	}
}

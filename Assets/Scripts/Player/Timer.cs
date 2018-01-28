using System;
using System.Collections;
using System.Collections.Generic;
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

	public void SetSeconds(float seconds)
	{
		var t = TimeSpan.FromSeconds( seconds );
		label.text = string.Format("{0:D2}:{1:D2}s",
			t.Minutes,
			t.Seconds);
	}
}

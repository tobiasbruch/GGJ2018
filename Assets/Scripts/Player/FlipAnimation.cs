﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAnimation : MonoBehaviour {
	[SerializeField]
	private SpriteRenderer _spriteRenderer;
	[SerializeField]
	private Animator _animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckForOrientation();
	}

	void CheckForOrientation(){
		if(transform.eulerAngles.z < 0 || transform.eulerAngles.z > 180){
			_spriteRenderer.flipY = true;
		} else {
			_spriteRenderer.flipY = false;
		}

		float angleToFlip = Mathf.Min(Mathf.Abs(180 - transform.eulerAngles.z), Mathf.Abs(0 - transform.eulerAngles.z));

		_animator.SetFloat("angleToFlip", angleToFlip);
	}
}

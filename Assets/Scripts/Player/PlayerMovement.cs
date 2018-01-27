using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _speed;

	private float _xInput;
	private float _yInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
	}

	void GetInput(){
		_xInput = Input.GetAxis("Horizontal");
		_yInput = Input.GetAxis("Vertical");
	}

	void ProcessInput(){
		transform.position += new Vector3(_xInput, _yInput) * _speed * Time.deltaTime;
	}
}

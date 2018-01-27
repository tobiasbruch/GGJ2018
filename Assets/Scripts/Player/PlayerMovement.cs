using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _flapVelocity;
	[SerializeField]
	private float _turnRate;

	private float _xInput;
	private float _xVelocity;

	public Rigidbody2D _rigidbody;



	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = new Vector2(_speed, _flapVelocity);
	}

	// Update is called once per frame
	void Update () {
		GetInput();
		transform.right = _rigidbody.velocity;
	}

	void FixedUpdate(){
		ProcessInput();
	}

	public void ConnectItem(Joint2D joint)
	{
		//joint.connectedBody = _rigidbody;
	}

	void GetInput(){
		float newXInput =  Input.GetAxis("Horizontal");
		if(newXInput != _xInput && newXInput != 0){
			_xInput = newXInput;
			float xVel = 0;
			if(_xInput > 0){
				xVel = _speed;
			} else if(_xInput < 0){
				xVel = -_speed;
			}
			_rigidbody.velocity = new Vector2(xVel, _flapVelocity);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Locator.Get<TaskManager>().Drop();
		}
		/*
		_xInput = Input.GetAxis("Horizontal");

		if(!Mathf.Approximately(_xInput, 0)){
			_xVelocity = Mathf.MoveTowards(_xVelocity, Mathf.Sign(_xInput) * _speed, Time.deltaTime * _turnRate);
		}*/
	}

	void ProcessInput(){
		//_rigidbody.velocity = new Vector2(_xVelocity, Input.GetKeyDown(KeyCode.Space) ? _flapVelocity : _rigidbody.velocity.y);
	}
}

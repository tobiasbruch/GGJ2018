using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _flapVelocity;
	[SerializeField]
	private float _descentForce;
	[SerializeField]
	private float _flapVelocityUp;

	private Rigidbody2D _rigidbody;

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

	void GetInput(){
		if(Input.GetKeyDown(KeyCode.A)){
			_rigidbody.velocity = new Vector2(-_speed, _flapVelocity);
		} else if(Input.GetKeyDown(KeyCode.D)){
			_rigidbody.velocity = new Vector2(_speed, _flapVelocity);
		} else if(Input.GetKeyDown(KeyCode.W)){
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _flapVelocityUp);
		} else if(Input.GetKeyDown(KeyCode.S)){
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Min(-1, _rigidbody.velocity.y));
		}
	}

	void ProcessInput(){
		if(Input.GetKey(KeyCode.S)){
			_rigidbody.AddForce(Vector2.down * Time.fixedDeltaTime * _descentForce);
		}
	}
}

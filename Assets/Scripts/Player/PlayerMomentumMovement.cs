using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMomentumMovement : MonoBehaviour {

	[SerializeField]
	private float _force;
	[SerializeField]
	private float _turnRate;
	[SerializeField]
	private float _conversionRate;

	private Vector2 _input;


	public Rigidbody2D _rigidbody;
	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		GetInput();
		ApplyRotation();

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Locator.Get<TaskManager>().Drop();
		}
	}

	void FixedUpdate(){
		ProcessInput();
	}

	void ApplyRotation(){
		if(_input != Vector2.zero){
			float angle = Vector2.SignedAngle(Vector2.up, _input);
			float velocity = _rigidbody.velocity.magnitude;
			transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle, _turnRate * Time.deltaTime));
		} else {
			float angle = Vector2.SignedAngle(Vector2.up, _rigidbody.velocity);
			transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle, _turnRate * Time.deltaTime));
		}

	}
	void GetInput(){
		_input.x = Input.GetAxis("Horizontal");
		_input.y = Input.GetAxis("Vertical");
	}

	void ProcessInput(){
		if(_input != Vector2.zero){
			Vector2 vel = _rigidbody.velocity;
			_rigidbody.AddForce(-_rigidbody.velocity * _conversionRate);
			_rigidbody.AddForce(transform.right * vel.magnitude * _conversionRate);
		}
		Vector3 force = _input.normalized * (1 - Vector2.Dot(transform.right, _input)) * _force;

		_rigidbody.AddForce(force);
	}
}

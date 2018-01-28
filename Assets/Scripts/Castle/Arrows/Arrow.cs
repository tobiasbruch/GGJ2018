using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	
	[SerializeField]
	private float _startingForce;
	[SerializeField]
	private AudioClip _hitClip;

	private Rigidbody2D _rigidbody;
	

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.AddForce(transform.forward * _startingForce, ForceMode2D.Impulse);
	}
	
	void Update(){
		transform.up = _rigidbody.velocity;
	}

	void OnCollisionEnter2D(Collision2D collision){
		AudioSource otherSource = collision.gameObject.GetComponentInChildren<AudioSource>();
		if(otherSource){
			otherSource.PlayOneShot(_hitClip, 2f);
		}
		Destroy(gameObject);
	}
}

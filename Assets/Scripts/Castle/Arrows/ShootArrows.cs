using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour {

	[SerializeField]
	private GameObject _arrowPrefab;
	[SerializeField]
	private float _shootingMinFrequency;
	[SerializeField]
	private float _shootingMaxFrequency;
	[SerializeField]
	private float _aimSpread;
	[SerializeField]
	private float _minDistance = 2f;

	private Transform _target;
	private Coroutine _shootingRoutine;

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<PlayerMovement>()){
			_target = collider.transform;
			if(_shootingRoutine != null){
				StopCoroutine(_shootingRoutine);
			}
			_shootingRoutine = StartCoroutine(RepeatShoot());
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.GetComponent<PlayerMovement>()){
			_target = null;
			StopCoroutine(_shootingRoutine);
		}
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator RepeatShoot(){
		while(true){
			yield return new WaitForSeconds(Random.Range(_shootingMinFrequency / Locator.Get<TaskManager>().hate, _shootingMaxFrequency / Locator.Get<TaskManager>().hate));

			if(Vector3.Distance(Locator.Get<PlayerMovement>().transform.position, this.transform.position) > _minDistance)
				Shoot();
		}
	}

	void Shoot(){
		if(_target){
			GameObject instance = Instantiate(_arrowPrefab, transform.position, Quaternion.LookRotation(_target.position - transform.position, Vector3.forward));
			instance.transform.LookAt(_target.position + new Vector3(0, Random.Range(0f, 1f) * (_target.position - transform.position).magnitude * _aimSpread, 0));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour {

	[SerializeField]
	private GameObject _arrowPrefab;
	[SerializeField]
	private GameObject _windUpPrefab;
	[SerializeField]
	private float _shootingMinFrequency;
	[SerializeField]
	private float _shootingMaxFrequency;
	[SerializeField]
	private float _aimSpread;
	[SerializeField]
	private float _minDistance = 2f;
	[SerializeField]
	private float _windUpDuration = 1.3333f;
	[SerializeField]
	private float _shotAnimationPoint = .75f;

	private bool _isInRange;
	private Transform _target;

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<PlayerMovement>()){
			_isInRange = true;
			StartCoroutine(RepeatShoot());
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.GetComponent<PlayerMovement>()){
			_isInRange = false;
		}
	}
	// Use this for initialization
	void Start () {
		_target = Locator.Get<PlayerMomentumMovement>().transform;
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator RepeatShoot(){
		while(_isInRange && Vector3.Distance(Locator.Get<PlayerMovement>().transform.position, this.transform.position) > _minDistance){
			float totalWaitTime = (Random.Range(_shootingMinFrequency / Locator.Get<TaskManager>().hate, _shootingMaxFrequency / Locator.Get<TaskManager>().hate));
			yield return new WaitForSeconds(totalWaitTime -_windUpDuration * _shotAnimationPoint);
			WindupShot shot = Instantiate(_windUpPrefab,transform, false).GetComponent<WindupShot>();
			shot._target = _target;
			shot.KillIn(_windUpDuration);
			yield return new WaitForSeconds(_windUpDuration * _shotAnimationPoint);

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

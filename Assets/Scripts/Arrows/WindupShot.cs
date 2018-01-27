using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WindupShot : MonoBehaviour {
	public Transform _target;
	
	public void KillIn(float waitTime){
		StartCoroutine(WaitAndKillIn(waitTime));
	}

	IEnumerator WaitAndKillIn(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(_target){
			transform.up = _target.position - transform.position;
		}
	}
}

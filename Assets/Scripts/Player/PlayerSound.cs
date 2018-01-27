using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	[SerializeField]
	private AudioClip[] _wingFlapping;
	[SerializeField]
	private AudioSource _wingFlappingAudioSource;


	public void PlayRandomWingFlapping(){
		_wingFlappingAudioSource.PlayOneShot(_wingFlapping[Random.Range(0, _wingFlapping.Length)]);
	}
}

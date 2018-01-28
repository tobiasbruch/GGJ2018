using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour {

	public float minTime = 2;
	public float maxTime = 5;

	public float minSpeed = 1;
	public float maxSpeed = 2;

	public Cloud[] prefabs;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnClouds());
	}

	IEnumerator SpawnClouds()
	{
		var orderInLayer = -5;
		while(true)
		{
			var c = prefabs[Random.Range(0, prefabs.Length)];

			var cloud = Instantiate(c, this.transform.position, Quaternion.identity);
			cloud.speed = Random.Range(minSpeed, maxSpeed);

			var pos = cloud.transform.position;

			pos.y = Random.Range(transform.position.y -1, transform.position.y +1);

			cloud.transform.position = pos;

			cloud.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
			orderInLayer--;

			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
		}
	}
}

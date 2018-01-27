using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Arrow>()){
			Destroy(gameObject);
			Locator.Get<Game>().GameOver();
		}
	}
}

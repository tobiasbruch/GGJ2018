using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision){
		var arrow = collision.gameObject.GetComponent<Arrow>();
		if(arrow){
			//Destroy(gameObject);
			var r= collision.gameObject.GetComponent<Rigidbody2D>();
			this.GetComponent<Rigidbody2D>().AddForce(r.velocity * 1.6f, ForceMode2D.Impulse);
		//	Locator.Get<Game>().GameOver();
		}
	}
}

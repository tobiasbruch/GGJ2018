using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RequestSpeechBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {

		transform.DOPunchScale(Vector3.one * 1.005f, 1f, 0).OnComplete(()=>{
			transform.DOLocalMoveY(1, 4f);
			GetComponent<SpriteRenderer>().DOFade(0, 4f).OnComplete(()=>{
				Destroy(transform.parent.gameObject);
			});
		});
	}
	
}

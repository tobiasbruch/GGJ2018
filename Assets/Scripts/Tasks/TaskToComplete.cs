using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TaskToComplete : MonoBehaviour
{
	[SerializeField] public double coinsReward;
	[SerializeField] public int hate;

	[SerializeField] TextMeshPro label;

	[HideInInspector] public int targetId;

	public void SetTarget(int targetId)
	{
		this.targetId = targetId;
		label.text = targetId.ToString();
	}

	public bool pickedUp = false;
	public void PickedUp()
	{
		pickedUp = true;
	}

	public void Drop()
	{
		pickedUp = false;
		var r = this.gameObject.AddComponent<Rigidbody2D>();
		r.velocity = Locator.Get<PlayerMovement>()._rigidbody.velocity;
		r.mass /=2;
	}

	void Update()
	{
		if(pickedUp)
		{
			var p = Locator.Get<PlayerMovement>();
			var toPos = p.transform.position;
			var vel = new Vector3(p._rigidbody.velocity.x, p._rigidbody.velocity.y, 0);
			vel.Normalize();
			vel /= 2;
			toPos -= vel;
			transform.position = Vector3.MoveTowards(transform.position, toPos, 2.8f * Time.deltaTime);
		}
	}
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TaskToComplete : MonoBehaviour
{
	[SerializeField] public double coinsReward;
	[SerializeField] public int hate;

	[SerializeField] TextMeshPro label;

	[SerializeField] GameObject root;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Sprite[] allSprites;

	[SerializeField] ParticleSystem particleSystem;

	[HideInInspector] public int targetId;


	void Start()
	{
		do
		{
			var rnd = Random.Range(0, allSprites.Length-1);
			spriteRenderer.sprite = allSprites[rnd];
		} while(spriteRenderer.sprite == null);

		var c = spriteRenderer.bounds.center;
		c.x += spriteRenderer.bounds.size.x / 4;
		root.transform.position = c;

	}
	public void SetTarget(int targetId)
	{
		this.targetId = targetId;
		label.text = targetId.ToString();
	}

	public bool pickedUp = false;
	public void PickedUp()
	{
		pickedUp = true;
		particleSystem.gameObject.SetActive(false);
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
			var p = Locator.Get<PlayerMomentumMovement>();
			if(p != null)
			{
				var toPos = p.transform.position;
				var vel = new Vector3(p._rigidbody.velocity.x, p._rigidbody.velocity.y, 0);
				vel.Normalize();
				vel /= 2;
				toPos -= vel;
				transform.position = Vector3.MoveTowards(transform.position, toPos, 2.8f * Time.deltaTime);
			}
		}
	}
}

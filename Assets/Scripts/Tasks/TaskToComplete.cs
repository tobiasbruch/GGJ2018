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

	[SerializeField] float[] weights;


	[SerializeField] ParticleSystem particleSystem;

	[HideInInspector] public int targetId;

	int imageId;

	void Start()
	{
		do
		{
			imageId = Random.Range(0, allSprites.Length-1);
			spriteRenderer.sprite = allSprites[imageId];
		} while(spriteRenderer.sprite == null);

		//var c = spriteRenderer.bounds.center;
		//c.x += spriteRenderer.bounds.size.x / 4;
		//root.transform.position = c;

	}
	public void SetTarget(int targetId)
	{
		this.targetId = targetId;
		//label.text = targetId.ToString();
	}

	public bool pickedUp = false;
	public void PickedUp()
	{
		pickedUp = true;
		particleSystem.gameObject.SetActive(false);
		Locator.Get<PlayerMomentumMovement>()._rigidbody.mass += weights[imageId]/10;
	}

	public void Drop()
	{
		pickedUp = false;
		var r = this.gameObject.AddComponent<Rigidbody2D>();
		r.velocity = Locator.Get<PlayerMomentumMovement>()._rigidbody.velocity;
		r.mass /=2;
		Locator.Get<PlayerMomentumMovement>()._rigidbody.mass -= weights[imageId]/10;
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
				transform.position = Vector3.MoveTowards(transform.position, toPos, 3.2f * Time.deltaTime);
			}
		}
	}
}

using UnityEngine;

public class Cloud : MonoBehaviour
{
	public float speed;

	void Update()
	{
		var pos = transform.position;
		pos.x -= speed/5 * Time.deltaTime;
		transform.position = pos;
	}
}
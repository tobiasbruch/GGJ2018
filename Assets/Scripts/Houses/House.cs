using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
	const float completeAtDistance = .5f;

	public TaskToComplete task;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(task != null && Vector3.Distance(Locator.Get<PlayerMovement>().transform.position, transform.position) < completeAtDistance)
		{
			Locator.Get<TaskManager>().CompletedTask(task);
			this.task = null;
		}
	}
}
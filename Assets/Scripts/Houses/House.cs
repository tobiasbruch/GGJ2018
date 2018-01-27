using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
	const float completeAtDistance = 1.3f;

	public int id => int.Parse(name.Substring(name.Length-1));

	public TaskToComplete availableTask {get; private set;}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		var player = Locator.Get<PlayerMovement>();

		if(player == null) return;
		if(Vector3.Distance(player.transform.position, transform.position) < completeAtDistance)
		{
			if(availableTask != null)
			{
				Locator.Get<TaskManager>().PickupTask(availableTask);
				availableTask = null;
			}
			else
			{
				Locator.Get<TaskManager>().CanCompleteTask(this);
			}
		}
	}

	public void GiveTask(TaskToComplete task)
	{
		this.availableTask = task;
	}
}
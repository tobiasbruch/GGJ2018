using System.Collections;
using DG.Tweening;
using UnityEngine;

public class House : MonoBehaviour
{
	[SerializeField] ShootArrows shootArrows;

	const float completeAtDistance = 1.3f;

	public int id => int.Parse(name.Substring(name.Length-1));

	public float loveMeter;

	public TaskToComplete availableTask {get; private set;}

	// Update is called once per frame
	void Update ()
	{
		var player = Locator.Get<PlayerMomentumMovement>();

		if(player == null) return;

		if(availableTask != null && Vector3.Distance(player.transform.position, availableTask.transform.position) < completeAtDistance)
		{
			if(availableTask != null && Locator.Get<TaskManager>().activeTask == null)
			{
				Locator.Get<TaskManager>().PickupTask(availableTask);
				availableTask = null;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var task = collider.GetComponent<TaskToComplete>();
		if(task != null && task.targetId == this.id)
		{
			Locator.Get<TaskManager>().CompleteTask(task);
			GetComponent<Animation>().Play("HouseBounce");
		}
	}


	public void GiveTask(TaskToComplete task)
	{
		this.availableTask = task;
	}
}
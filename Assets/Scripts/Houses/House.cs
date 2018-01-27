using System.Collections;
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

		var list = Locator.Get<TaskManager>().droppedTasks;
		for (var i = list.Count - 1; i >= 0; i--)
		{
			var task = list[i];
			if (task != null && !task.pickedUp && task.targetId == this.id &&
			    Vector3.Distance(task.transform.position, transform.position) < completeAtDistance)
				Locator.Get<TaskManager>().CompleteTask(task);
		}

		if(Vector3.Distance(player.transform.position, transform.position) < completeAtDistance)
		{
			if(availableTask != null && Locator.Get<TaskManager>().activeTask == null)
			{
				Locator.Get<TaskManager>().PickupTask(availableTask);
				availableTask = null;
			}
		}
	}

	public void GiveTask(TaskToComplete task)
	{
		this.availableTask = task;
	}
}
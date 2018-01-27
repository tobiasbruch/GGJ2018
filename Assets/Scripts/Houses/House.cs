using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
	[SerializeField] ShootArrows shootArrows;

	const float completeAtDistance = 1.3f;

	public int id => int.Parse(name.Substring(name.Length-1));

	public float loveMeter;

	public TaskToComplete availableTask {get; private set;}

	// Use this for initialization
	void Start ()
	{
		shootArrows.modifier = 0;
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

	public void AddLove(int love)
	{
		loveMeter += love;

		if(love < -100) loveMeter = -100;
		if(love > 100) loveMeter = 100;

		if(loveMeter <= 0)
		{
			var hate = Mathf.Abs(loveMeter);
			shootArrows.enabled = true;
			shootArrows.modifier =  hate / 100 * 2;
		} else
		{
			shootArrows.enabled = false;
		}
	}

	public void GiveTask(TaskToComplete task)
	{
		this.availableTask = task;
	}
}
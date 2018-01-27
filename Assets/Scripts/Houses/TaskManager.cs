using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	[SerializeField] float taskFrequencySecondsMin = 10;
	[SerializeField] float taskFrequencySecondsMax = 15;
	[SerializeField] float maskTasks = 2;
	[SerializeField] float dontCreateTaskAtDistanceToHouse = 1.5f;

	public List<House> houses;

	public List<TaskToComplete> tasks;

	public List<TaskToComplete> activeTasks;

	public void Init()
	{
		foreach (var taskToComplete in tasks)
		{
			taskToComplete.gameObject.SetActive(false);
		}
		StartCoroutine(CreateTasks());
	}

	IEnumerator CreateTasks()
	{
		while(true)
		{
			if(houses.FindAll(t => t.availableTask != null).Count <= maskTasks )
			{
				SetRandomTask();
				yield return new WaitForSeconds(Random.Range(taskFrequencySecondsMin, taskFrequencySecondsMax));
			}
			else
			{
				yield return new WaitForSeconds(Random.Range(2,5));

			}
		}
	}

	public void SetRandomTask(int exclude = -1)
	{
		var player = Locator.Get<PlayerMovement>();

		if(player == null) return;
		var allowedHouses = houses.Where(x => x.availableTask == null && x.id != exclude &&
		      Vector3.Distance(x.transform.position, player.transform.position) > dontCreateTaskAtDistanceToHouse).ToList();

		var houseGivenTask = houses[Random.Range(0, allowedHouses.Count)];

		var otherHouses = houses.Where(t => t != houseGivenTask).ToList();
		var toOtherHouse = otherHouses[Random.Range(0, otherHouses.Count)];

		var task = Instantiate(tasks[Random.Range(0, tasks.Count)]);

		task.gameObject.SetActive(true);
		task.transform.localScale = Vector3.one;
		task.transform.DOPunchScale(Vector3.one * 2, .5f, 1, .1f);
		task.SetTarget(toOtherHouse.id);

		task.transform.position = houseGivenTask.transform.position + new Vector3(1,1, 0);
		houseGivenTask.GiveTask(task);
	}

	public void PickupTask(TaskToComplete task)
	{
		activeTasks.Add(task);
		var pos = Camera.main.ViewportToWorldPoint(new Vector3(.05f, 1 - (.02f + activeTasks.Count * .08f), 0));
		pos.z = 0;
		task.transform.DOMove(pos, .5f).SetEase(Ease.InOutSine);
	}

	public void CanCompleteTask(House house)
	{
		var task = activeTasks.Find(t => t.targetId == house.id);
		if(task != null)
		{
			Destroy(task.gameObject);
			house.AddLove(task.love);
			activeTasks.Remove(task);
			Locator.Get<Resources>().AddCoins(task.coinsReward);
		}

	}
}

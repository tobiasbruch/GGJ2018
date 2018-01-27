using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	public List<House> houses;

	public List<TaskToComplete> tasks;


	public List<TaskToComplete> activeTasks;

	public void Init()
	{
		foreach (var taskToComplete in tasks)
		{
			taskToComplete.gameObject.SetActive(false);
		}
	}

	public void StartRandomTask(int exclude = -1)
	{
		int randomId;
		do
		{
			randomId = Random.Range(0, houses.Count);
		} while(randomId == exclude);

		var task = tasks[randomId];
		tasks[randomId].gameObject.SetActive(true);

		activeTasks.Add(task);

		task.transform.localScale = Vector3.one;

		task.transform.DOPunchScale(Vector3.one * 2, .5f, 1, .1f);
		houses[randomId].task = task;
	}

	public void CompletedTask(TaskToComplete task)
	{
		task.gameObject.SetActive(false);
		activeTasks.Remove(task);
		StartRandomTask(task.id);
		Locator.Get<Resources>().AddCoins(0.05);
	}
}

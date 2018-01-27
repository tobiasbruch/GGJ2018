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
	[SerializeField] GameObject taskListContainer;

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

		var houseGivenTask = allowedHouses[Random.Range(0, allowedHouses.Count)];

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

		var last = activeTasks.LastOrDefault();

		var pos = new Vector3();

		if(taskListContainer.transform.childCount > 0)
			pos.y = taskListContainer.transform.GetChild(taskListContainer.transform.childCount-1)
				        .transform.position.y -.7f;

		//if(last != null)
		//	pos.y = last.transform.position.y - 20;
		task.transform.SetParent(taskListContainer.transform);

		task.transform.DOLocalMove(pos, .5f).SetEase(Ease.InOutSine);
	}

	void Update()
	{

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

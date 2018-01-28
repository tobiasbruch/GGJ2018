using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	[System.Serializable]
	public class RequestSpeechBubbles{
		public int _id;
		public Sprite[] _speechBubbles;
	}
	public float hate;

	[SerializeField] GameObject[] _receiveSpeechBubble;
	[SerializeField] GameObject _requestSpeechBubblePrefab;
	[SerializeField] RequestSpeechBubbles[] _requestSpeechBubbles;
	[SerializeField] private float[] _hateThresholds;
	[SerializeField] float taskFrequencySecondsMin = 10;
	[SerializeField] float taskFrequencySecondsMax = 15;
	[SerializeField] float maskTasks = 2;
	[SerializeField] float dontCreateTaskAtDistanceToHouse = 1.5f;
	[SerializeField] float hateIncrement = .3f;

	[SerializeField] GameObject taskListContainer;

	[SerializeField] private AudioClip[] _pickUpLetterClips;

	public List<House> houses;

	public List<TaskToComplete> tasks;

	public List<TaskToComplete> activeTasks;

	public List<TaskToComplete> droppedTasks;

	public TaskToComplete activeTask => activeTasks.Count > 0 ? activeTasks[0] : null;

	public int pickedUpTasks = 0;

	public int completedCount = 0;

	private AudioSource _audioSource;
	public void Init()
	{
		foreach (var taskToComplete in tasks)
		{
			taskToComplete.gameObject.SetActive(false);
		}

		StartCoroutine(CreateTasks());
	}

	void Start(){
		_audioSource = GetComponent<AudioSource>();
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
		//Create Speechbubble
		int hateLevel = 0;
		for(int i = 0; i < _hateThresholds.Length; i++){
			if(_hateThresholds[i] > hate){
				hateLevel = i;
				break;
			}
		}
		RequestSpeechBubbles speechbubbles = null;
		foreach(RequestSpeechBubbles item in _requestSpeechBubbles){
			if(item._id == task.targetId){
				speechbubbles = item;
				break;
			}
		}
		if(speechbubbles != null){
		Instantiate(_requestSpeechBubblePrefab, task.transform.position - new Vector3(1, 0, 0), Quaternion.identity).
			GetComponentInChildren<SpriteRenderer>().sprite = speechbubbles._speechBubbles[Mathf.Min(hateLevel, speechbubbles._speechBubbles.Length)];
		}

		activeTasks.Add(task);

		var last = activeTasks.LastOrDefault();

		var pos = new Vector3();

		if(taskListContainer.transform.childCount > 0)
			pos.y = taskListContainer.transform.GetChild(taskListContainer.transform.childCount-1)
				        .transform.localPosition.y -.7f;

		//if(last != null)
		//	pos.y = last.transform.position.y - 20;
		//task.transform.SetParent(taskListContainer.transform);

		//task.transform.DOLocalMove(pos, .5f).SetEase(Ease.InOutSine);
		var p = Locator.Get<PlayerMovement>();
		p.ConnectItem(task.GetComponent<Joint2D>());
		task.transform.position = p.transform.position;
		task.transform.localScale *= .6f;
		task.PickedUp();

		var otherHouse = houses.Find(t => t.id == task.targetId);

		if(completedCount == 0)
			otherHouse.LightUp();


		_audioSource.PlayOneShot(_pickUpLetterClips[Random.Range(0, _pickUpLetterClips.Length)]);
	}

	public void Drop()
	{
		if(activeTask != null)
		{
			droppedTasks.Add(activeTask);
			activeTask.Drop();
			activeTasks.Remove(activeTask);
		}
	}

	void Update()
	{
		for (var i = droppedTasks.Count - 1; i >= 0; i--)
		{
			var taskToComplete = droppedTasks[i];
			if (taskToComplete != null && taskToComplete.transform.position.y < -10)
			{
				Destroy(taskToComplete);
				droppedTasks.RemoveAt(i);
				ResetParticles();
			}
		}
	}

	void ResetParticles()
	{
		houses.ForEach(t => t.ResetLightUp());
	}


	public void CompleteTask(TaskToComplete task)
	{
		completedCount++;
		House targetHouse = null;
		foreach(var house in houses){
			if(house.id == task.targetId){
				targetHouse = house;
				break;
			}
		}
		if(targetHouse){
			Instantiate(_receiveSpeechBubble[Random.Range(0, _receiveSpeechBubble.Length)], targetHouse.transform.position + Vector3.up, Quaternion.identity);
		}
		hate += hateIncrement;
		Locator.Get<Resources>().AddCoins(task.coinsReward);
		Locator.Get<Timer>().Bounce();
		droppedTasks.Remove(task);
		Destroy(task.gameObject);
		ResetParticles();
		Locator.Get<Game>().AddTime();
	}
}

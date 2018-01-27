using TMPro;
using UnityEngine;


public class TaskToComplete : MonoBehaviour
{
	[SerializeField] float coinsReward;
	[SerializeField] float love;

	[SerializeField] TextMeshPro label;

	[HideInInspector] public int targetId;

	public void SetTarget(int targetId)
	{
		this.targetId = targetId;
		label.text = targetId.ToString();
	}
}

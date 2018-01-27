using UnityEngine;


public class TaskToComplete : MonoBehaviour
{
	public int id
	{
		get
		{
			return this.name[this.name.Length-1];
		}
	}
}

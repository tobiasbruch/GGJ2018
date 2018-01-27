using UnityEngine;


public class TaskToComplete : MonoBehaviour
{
	public int id
	{
		get
		{
			return int.Parse(name.Substring(name.Length-1));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI label;

	double coins = 0;

	void Start()
	{
		label.text = "0";
	}

	public void AddCoins(double coins)
	{
		this.coins += coins;
		label.text = this.coins.ToString();
	}
}

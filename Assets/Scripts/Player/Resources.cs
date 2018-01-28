using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI label;

	public double coins = 0;

	void Start()
	{
		label.text = "0";
	}

	public void AddCoins(double coins)
	{
		this.coins += coins;
		label.text = this.coins.ToString();
		transform.DOKill();
		transform.DOPunchScale(Vector3.one * 1.2f, .5f, 1, .1f);
		transform.localScale = Vector3.one;
	}
}

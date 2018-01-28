using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	[SerializeField] Image image;
	[SerializeField] Button button;

	List<Vector3> housePositions = new List<Vector3>();

	bool started = false;
	// Use this for initialization
	void Start ()
	{
		Locator.Get<PlayerMomentumMovement>().gameObject.SetActive(false);

		var houses = Locator.Get<TaskManager>().houses;
		foreach (var house in houses)
			housePositions.Add(house.transform.position);

		foreach (var house in houses)
			house.transform.position += Vector3.up * 15;

		button.onClick.AddListener(() =>
		{
			StartCoroutine(DoFadeOut());
		});
	}

	public void Update()
	{
		if(Input.anyKeyDown)
		{
			StartCoroutine(DoFadeOut());
		}
	}

	IEnumerator DoFadeOut()
	{
		if(started) yield break;
		started = true;
		image.DOFade(0, .5f);
		yield return new WaitForSeconds(.5f);


		var houses = Locator.Get<TaskManager>().houses;

		for (var i = 0; i < houses.Count; i++)
		{
			var house = houses[i];
			house.transform.DOMove(housePositions[i], .5f).SetEase(Ease.InCubic);
			StartCoroutine(DoScale(house.transform));
			yield return new WaitForSeconds(.10f);
		}

		yield return new WaitForSeconds(.7f);

		Locator.Get<PlayerMomentumMovement>().gameObject.SetActive(true);

		Locator.Get<PlayerMomentumMovement>().transform.DOPunchScale(Vector3.one * 1.1f, .5f, 1, 1);

		yield return new WaitForSeconds(2);
		Locator.Get<TaskManager>().Init();

		Destroy(gameObject);
	}

	IEnumerator DoScale(Transform t)
	{
		yield return new WaitForSeconds(.35f);
		//t.DOPunchScale(new Vector3(1.3f, .7f, 1), .6f, 2, .1f);
		t.GetComponent<Animation>().Play("HouseSqueeze");
	}
}

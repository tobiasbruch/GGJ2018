using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	[SerializeField] Image image;
	[SerializeField] Button button;
	[SerializeField] AudioSource _musicSource;
	[SerializeField] AudioClip _transition;
	[SerializeField] AudioClip _mainTheme;


	[SerializeField]
	private GameObject birdAppear;
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
		if(Input.anyKeyDown && !started)
		{
			StartCoroutine(DoFadeOut());
			StartCoroutine(ChangeToMainTheme());
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

		var p = Locator.Get<PlayerMomentumMovement>();
		p.gameObject.SetActive(true);
		p.transform.DOPunchScale(Vector3.one * 1.1f, .5f, 1);
		var pos = p.transform.position;
		pos.y += 1f;
		Instantiate(birdAppear, pos, Quaternion.identity);

		yield return new WaitForSeconds(2);
		Locator.Get<TaskManager>().Init();

		Destroy(gameObject);
	}

	IEnumerator ChangeToMainTheme(){
		/*_musicSource.Stop();
		_musicSource.clip = _transition;
		_musicSource.Play();
		yield return new WaitForSeconds(_transition.length);*/
		yield return null;
		_musicSource.Stop();
		_musicSource.clip = _mainTheme;
		_musicSource.Play();
	}

	IEnumerator DoScale(Transform t)
	{
		yield return new WaitForSeconds(.35f);
		//t.DOPunchScale(new Vector3(1.3f, .7f, 1), .6f, 2, .1f);
		t.GetComponent<Animation>().Play("HouseSqueeze");
	}
}

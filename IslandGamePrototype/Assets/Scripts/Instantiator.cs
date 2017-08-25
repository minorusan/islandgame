using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Instantiator : MonoBehaviour
{

	public GameObject[] prefabs;
	public float Rate = 30f;
	// Use this for initialization
	void Start()
	{
		StartCoroutine (InstatiateDudes ());
	}

	private IEnumerator InstatiateDudes()
	{
		while (true)
		{
			Instantiate (prefabs [Random.Range (0, prefabs.Length)], transform);

			yield return new WaitForSeconds (Rate);
		}
	}
}

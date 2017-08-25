using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroyer : MonoBehaviour
{

	private void OnTriggerEnter(Collider triger)
	{
		if (triger.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
	}
}

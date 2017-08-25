using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Destroyer : MonoBehaviour
{
	private void Awake()
	{
		var body = gameObject.AddComponent <Rigidbody> ();
		body.useGravity = false;
	}

	private void OnTriggerEnter(Collider triger)
	{
		if (triger.gameObject.tag == "Player")
		{
			//Destroy (gameObject);
		}
	}
}

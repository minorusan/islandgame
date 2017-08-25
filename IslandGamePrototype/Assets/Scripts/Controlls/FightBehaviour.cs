using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightBehaviour : MonoBehaviour
{
	private List<Collider> _inRange = new List<Collider> ();

	public BoxCollider fightDistance;
	public float force;

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.J))
		{
			foreach (var item in _inRange)
			{
				if (item != null)
				{
					item.GetComponent <HealthBehaviour> ().TakeHit (10f);
				}
			}
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		_inRange.Add (collision.gameObject.GetComponent <Collider> ());
	}

	void OnTriggerExit(Collider collision)
	{
		_inRange.Remove (collision.gameObject.GetComponent <Collider> ());
	}

	void OnTriggerStay(Collider collision)
	{
		if (!_inRange.Contains (collision.GetComponent <Collider> ()))
		{
			_inRange.Add (collision.gameObject.GetComponent <Collider> ());
		}
	}
}

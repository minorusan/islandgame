using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HoundsPackEgregor : MonoBehaviour
{
	private Transform _player;
	public NavMeshAgent[] _agents;

	void Start()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		StartCoroutine (ActivateDudes ());
		StartCoroutine (GiveTarget ());
	}

	private IEnumerator ActivateDudes()
	{
		for (int i = 0; i < _agents.Length; i++)
		{
			_agents [i].gameObject.SetActive (true);
			yield return new WaitForSeconds (0.2f);
		}
	}

	private IEnumerator GiveTarget()
	{
		while (true)
		{
			for (int i = 0; i < _agents.Length; i++)
			{
				if (_agents [i] != null && _agents [i].gameObject.activeInHierarchy)
				{
					_agents [i].SetDestination (_player.transform.position);
				}


			}
			yield return new WaitForSeconds (0.1f);
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;


public class LandPackEgregor : MonoBehaviour
{

	private Collider _packTrigger;
	private NavMeshAgent[] _agents;
	private Transform _player;
	public Material attackMaterial;
	public GameObject original;

	void Start()
	{
		_agents = GetComponentsInChildren <NavMeshAgent> ();
		_packTrigger = GetComponent <Collider> ();
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		StartCoroutine (GiveWanderingTargets ());
	}

	private void OnTriggerEnter(Collider trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			StopCoroutine (GiveWanderingTargets ());
			StartCoroutine (GiveTarget ());
			_packTrigger.enabled = false;
		}	
	}

	private void Update()
	{
		for (int i = 0; i < _agents.Length; i++)
		{
			if (_agents [i] != null)
			{
				return;
			}
		}

		if (Vector3.Distance (transform.position, _player.transform.position) > 20f)
		{
			var newone = GameObject.Instantiate (original);
			newone.transform.position = transform.position;
			Destroy (gameObject);
		}
	}

	private IEnumerator GiveTarget()
	{
		while (true)
		{
			for (int i = 0; i < _agents.Length; i++)
			{
				if (_agents [i] != null && _agents [i].isOnNavMesh && _agents [i].gameObject.activeInHierarchy)
				{
					_agents [i].SetDestination (_player.transform.position);
					_agents [i].GetComponent <Renderer> ().material = attackMaterial;
				}


			}
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator GiveWanderingTargets()
	{
		while (true)
		{
			for (int i = 0; i < _agents.Length; i++)
			{
				if (_agents [i] != null && _agents [i].isOnNavMesh)
				{
					Vector3 newPosition = RandomCircle (_agents [i].transform.position, Random.Range (1.5f, 3f));
					if (!_packTrigger.bounds.Contains (newPosition))
					{
						newPosition = _packTrigger.ClosestPoint (newPosition);
					}
					 
					_agents [i].SetDestination (newPosition);
				}

			}
			yield return new WaitForSeconds (2f);
		}
	}

	Vector3 RandomCircle(Vector3 center, float radius)
	{
		float ang = Random.value * 360;
		Vector3 pos = Vector3.zero;
		pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos (ang * Mathf.Deg2Rad);
		return pos;
	}
}

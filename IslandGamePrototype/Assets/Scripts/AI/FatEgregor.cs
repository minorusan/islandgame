using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.IMGUI.Controls;


public class FatEgregor : MonoBehaviour
{
	private Transform _player;
	private NavMeshAgent _agent;
	private Vector3 _lastPosition;
	public GameObject SwarmPrefab;
	// Use this for initialization
	void Start()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_agent = GetComponentInChildren<NavMeshAgent> ();
		StartCoroutine (GetPath ());
	}

	IEnumerator GetPath()
	{
		while (true)
		{
			if (_agent != null)
			{
				_agent.SetDestination (_player.position);
				_lastPosition = _agent.transform.position;
			}
			else
			{
				Invalidate ();
			}
			yield return new WaitForSeconds (0.2f);
		}
	}

	private void Invalidate()
	{
		var t = Instantiate (SwarmPrefab);
		t.transform.position = _lastPosition;
		t.gameObject.SetActive (true);
		StopAllCoroutines ();
	}
}

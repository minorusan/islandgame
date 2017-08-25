using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Policy;


public class IslandPackEgregor : MonoBehaviour
{
	private Vector3[] _smallOnesCoords;
	private Transform _player;
	public Material _aggressiveMaterial;

	public NavMeshAgent[] bigOnes;
	public NavMeshAgent[] smallOnes;
	private bool _aggressive;

	public float distance = 3f;

	void Start()
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;

		StartCoroutine (EnableSmallOnes ());

		StartCoroutine (GetCircleCoordsForSmallDudes ());
		StartCoroutine (GetCoordsForBiggerOnes ());
	}

	private void Update()
	{
		if (_aggressive)
		{
			return;
		}
		for (int i = 0; i < bigOnes.Length; i++)
		{
			if (bigOnes [i] == null)
			{
				_aggressive = true;
				StartCoroutine (GetAggressiveCoordsForSmallerDudes ());
				StopCoroutine (GetCircleCoordsForSmallDudes ());

				for (int j = 0; j < smallOnes.Length; j++)
				{
					if (smallOnes [j] != null)
					{
						smallOnes [j].gameObject.GetComponent <Renderer> ().material = _aggressiveMaterial;
					}

				}
				break;
			}
		}
	}


	private IEnumerator EnableSmallOnes()
	{
		for (int i = 0; i < smallOnes.Length; i++)
		{
			smallOnes [i].gameObject.SetActive (true);
			yield return new WaitForSeconds (0.2f);
		}
	}

	private IEnumerator GetCoordsForBiggerOnes()
	{
		while (true)
		{
			for (int i = 0; i < bigOnes.Length; i++)
			{
				if (bigOnes [i] != null && bigOnes [i].isOnNavMesh)
				{
					bigOnes [i].SetDestination (_player.position);
				}

			}

			yield return new WaitForSeconds (0.3f);
		}
	}

	private IEnumerator GetAggressiveCoordsForSmallerDudes()
	{
		while (true)
		{
			for (int i = 0; i < smallOnes.Length; i++)
			{
				if (smallOnes [i] != null && smallOnes [i].isOnNavMesh)
				{
					smallOnes [i].SetDestination (_player.position);
				}

			}

			yield return new WaitForSeconds (0.3f);
		}
	}

	private IEnumerator GetCircleCoordsForSmallDudes()
	{
		while (true)
		{
			var angle = 360f / smallOnes.Length;
			var lookDirection = _player.forward;

			_smallOnesCoords = new Vector3[smallOnes.Length];
			_smallOnesCoords [0] = (lookDirection * distance) + _player.position;

			for (int i = 1; i < smallOnes.Length; i++)
			{
				_smallOnesCoords [i] = RandomCircle (_player.position, distance, angle * i);
				if (smallOnes [i] != null && smallOnes [i].gameObject.activeInHierarchy)
				{
					smallOnes [i].SetDestination (_smallOnesCoords [i]);
				}
			}

			yield return new WaitForSeconds (1f);
		}

	}

	Vector3 RandomCircle(Vector3 center, float radius, float ang)
	{
		Vector3 pos = Vector3.zero;
		pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);

		pos.z = center.z + radius * Mathf.Cos (ang * Mathf.Deg2Rad);

		return pos;
	}
}

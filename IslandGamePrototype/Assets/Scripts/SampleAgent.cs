using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SampleAgent : MonoBehaviour
{

	public Transform target;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		GetComponent <NavMeshAgent> ().SetDestination (target.transform.position);
	}
}

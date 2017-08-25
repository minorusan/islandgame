using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;


public class HealthBehaviour : MonoBehaviour
{
	public GameObject bloodPrefab;
	public GameObject deathPrefab;
	public float currentHealth = 100f;

	public void TakeHit(float health)
	{
		currentHealth -= health;
		GetComponentInParent <NavMeshAgent> ().speed *= 0.7f;
		Invoke ("ReturnSpeed", 3f);
		if (currentHealth < 0)
		{
			
			var t = Instantiate (deathPrefab);
			t.transform.position = transform.position;
			Destroy (gameObject);
		}
		else
		{
			var blood = Instantiate (bloodPrefab, transform);
			blood.transform.rotation = Random.rotation;
		}
	}

	private void ReturnSpeed()
	{
		GetComponentInParent <NavMeshAgent> ().speed *= 1.3f;
	}

}

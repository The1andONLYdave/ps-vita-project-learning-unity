using UnityEngine;
using System.Collections;

public class HealthKit : MonoBehaviour {

	public float healthPoints = 1;

	HealthController healthController;
	// Use this for initialization
	void Start () {
		healthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
	}

	/*
	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			healthController.AddHealth(healthPoints);
			Destroy(gameObject);
		}
	}
*/
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			healthController.AddHealth(healthPoints);
			Destroy(gameObject);
		}
	}

}

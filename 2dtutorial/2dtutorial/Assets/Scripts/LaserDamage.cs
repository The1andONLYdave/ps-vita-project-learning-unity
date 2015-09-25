using UnityEngine;
using System.Collections;

public class LaserDamage : MonoBehaviour {

	public float damage = 1;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!other.CompareTag("Player"))
		{
			other.SendMessage("ApplyDamage",damage,SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}				
	}
}
